using Common;
using Model;
using Model.APIModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RET.API
{
    public partial class APIService : IAPIService
    {
        public LoginResponse Login(decimal lng, decimal lat, string equipment)
        {
            try
            {
                LoginResponse result = new LoginResponse();
                string token = SaveLoginInfo(Convert.ToDecimal(lng), Convert.ToDecimal(lat), equipment);
                
                result.status = ResultType.Success;
                result.message = "OK";
                result.token = token;

                return result;
            }
            catch (Exception ex)
            {
                LoginResponse result = new LoginResponse();
                result.status = ResultType.Exception;
                result.message = ex.Message;

                return result;
            }
            
        }

        public BaseResult Logout(string token)
        {
            BaseResult result = new BaseResult();
            try
            {
                LoginInfo loginInfo = GetLoginInfo(token);
                if (loginInfo == null)
                {
                    result.status = ResultType.TokenError;
                    result.message = "Token不正确";
                    return result;
                }
                if (SaveLogoutInfo(loginInfo))
                {
                    result.status = ResultType.Success;
                    result.message = "OK";
                    return result;
                }

                result.status = ResultType.Failed;
                result.message = "操作失败";

                return result;
            }
            catch (Exception ex)
            {
                result.status = ResultType.Exception;
                result.message = ex.Message;

                return result;
            }
        }

        private LoginInfo GetLoginInfo(string token)
        {

            try
            {
                LoginInfo loginInfo = context.Find<LoginInfo>(new Guid(token));

                return loginInfo;
            }
            catch
            {

                throw;
            }
        }

        private bool RecordBrowseInfo(string token, decimal lng, decimal lat, string api)
        {
            try
            {
                BrowseInfo browseInfo = new BrowseInfo();
                browseInfo.Id = Guid.NewGuid();
                if (!string.IsNullOrWhiteSpace(token))
                    browseInfo.LoginId = new Guid(token);
                browseInfo.Lng = lng;
                browseInfo.lat = lat;
                browseInfo.BrowseUrl = api;
                browseInfo.BrowseTime = DateTime.Now;
                context.CloseLog();
                context.Insert<BrowseInfo>(browseInfo);
                context.OpenLog();
                return true;
            }
            catch 
            {
                
                throw;
            }
        }

        private string SaveLoginInfo(decimal lng, decimal lat, string equipmentNum)
        {
            try
            {
                LoginInfo loginInfo = new LoginInfo();
                loginInfo.Id = Guid.NewGuid();
                loginInfo.EquipmentNum = equipmentNum;
                loginInfo.Lng = lng;
                loginInfo.lat = lat;
                loginInfo.LoginTime = DateTime.Now;

                context.Insert<LoginInfo>(loginInfo);

                return loginInfo.Id + "";
            }
            catch 
            {
                
                throw;
            }
        }

        private bool SaveLogoutInfo(LoginInfo loginInfo)
        {
            try
            {
                loginInfo.LogoutTime = DateTime.Now;
                return context.Update<LoginInfo>(loginInfo);
            }
            catch 
            {
                
                throw;
            }
        }

        private BaseResult AssertToken(string token)
        {
            try
            {
                
                BaseResult result = null;
                if (string.IsNullOrWhiteSpace(token))
                {
                    result = new BaseResult();
                    result.status = ResultType.TokenError;
                    result.message = "Token为空";
                }
                else
                {
                    LoginInfo loginInfo = GetLoginInfo(token);
                    if (loginInfo == null)
                    {
                        result = new BaseResult();
                        result.status = ResultType.TokenError;
                        result.message = "Token不正确";
                    }
                }
                return result;
            }
            catch 
            {
                
                throw;
            }
        }

        private void GetBrowseInfoResult<T>(string token, decimal lng, decimal lat, ref T result) where T : ReturnBaseModel
        {
            StackTrace st = new StackTrace(true);
            if (RecordBrowseInfo(token, lng, lat, st.GetFrame(1).GetMethod().Name))
            {
                result.status = ResultType.Success;
                result.message = "OK";
            }
            else
            {
                result.status = ResultType.Failed;
                result.message = "记录浏览页面不成功";
            }
            result.token = token;

            //result.status = ResultType.Success;
            //result.message = "OK";
            //result.token = token;
        }
    }
}