using Model;
using Model.APIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace RET.API
{
    public partial interface IAPIService
    {
        [WebInvoke(UriTemplate = "Login", ResponseFormat = WebMessageFormat.Json,BodyStyle=WebMessageBodyStyle.WrappedRequest)]
        LoginResponse Login(decimal lng, decimal lat, string equipment);

        [WebInvoke(UriTemplate = "Logout", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BaseResult Logout(string token);
    }
}