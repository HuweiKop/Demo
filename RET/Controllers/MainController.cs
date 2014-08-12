using BLL;
using Common;
using Core.Cache;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RET.Controllers
{
    public class MainController : Web.ControllerBase
    {
        //
        // GET: /Main/
        [AuthorityFilterAttribute("")]
        public ActionResult MainPage()
        {
            //Session["UserId"] = "C727B00B-BE97-4300-BCE6-885AB8A69158";
            Guid userId = new Guid(Session["UserId"] + "");
            ViewData["IsAdmin"] = Session["IsAdmin"];
            CacheHelper.Set("UserName", Session["UserId"]);
            List<string> listResource = AccountService.GetResourcesByUserId(userId);
            ViewData["permission"] = JsonTools.JsonSerializer(listResource);
            return View();
        }

    }
}
