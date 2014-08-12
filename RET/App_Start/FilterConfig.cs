using BLL;
using Common;
using Contract;
using Core.Cache;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace RET
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    //session过期处理
    public class SessionTimeOutFilterAttribute : ActionFilterAttribute
    {
        //是否ajax访问
        public string isAjax { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                HttpContextBase ctx = filterContext.HttpContext;

                if (ctx.Session != null)
                {
                    if (ctx.Session.IsNewSession)
                    {
                        //查询cookie中是否存在ASP.NET_SessionId字段，存在则表示session过期
                        string sessionCookie = ctx.Request.Headers["Cookie"];
                        if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                        {
                            if (filterContext.ActionParameters.ContainsKey("isAjax"))
                            {
                                isAjax = filterContext.ActionParameters["isAjax"].ToString();
                            }

                            string url = "~/Account/BackLogin?isAjax=" + isAjax;
                            //返回登录页面
                            filterContext.Result = new RedirectResult(url);
                        }
                    }
                    else if (filterContext.HttpContext.Session["UserId"] == null)
                    {
                        if (filterContext.ActionParameters.ContainsKey("isAjax"))
                        {
                            isAjax = filterContext.ActionParameters["isAjax"].ToString();
                        }

                        string url = "~/Account/BackLogin?isAjax=" + isAjax;
                        //返回登录页面
                        filterContext.Result = new RedirectResult(url);
                    }

                }
                CacheHelper.Get("UserName", () => { return filterContext.HttpContext.Session["UserId"]; });
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                //string s =ex.Message;
                throw;
            }
        }
    }

    //登录和权限处理
    public class AuthorityFilterAttribute : ActionFilterAttribute
    {
        //保存页面名称
        public string permission { get; set; }
        //保存用于区分不同页面的标志
        public string type { get; set; }

        public AuthorityFilterAttribute()
        {
        }

        public AuthorityFilterAttribute(string permission)
        {
            this.permission = permission;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpContextBase ctx = filterContext.HttpContext;
            Guid? userId = (Guid?)filterContext.HttpContext.Session["UserId"];
            if (userId == null)
            {
                //未登录时，返回登录页面
                //ctx.Response.Redirect("~/Login/BackLogin");

                string url = "~/Account/BackLogin";
                //返回登录页面
                filterContext.Result = new RedirectResult(url);
            }
            else
            {
                if (permission != null && permission != "")
                {
                    RedirectPage(ctx, (Guid)userId, permission, filterContext);
                }
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 查找当前用户是否有此页面的访问权限
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="loginPersonProfile">当前登录的用户</param>
        private void RedirectPage(HttpContextBase ctx, Guid userId, string paramPermission, ActionExecutingContext filterContext)
        {
            IAccountService accountServcie = new AccountService();
            List<string> listResource = accountServcie.GetResourcesByUserId(userId);

            if (!listResource.Contains(paramPermission))
            {
                string url = "~/Account/BackLogin";
                //返回登录页面
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}