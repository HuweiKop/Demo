using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RET.Controllers
{
    public class CityController : Web.ControllerBase
    {
        //
        // GET: /City/

        #region 页面加载

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("CityManagement", Order = 2)]
        public ActionResult CityList(City city, int page, string orderBy, string orderAs)
        {
            try
            {
                ViewData["Name"] = city.Name;
                PagedList<City> listCity = this.CityService.GetAllCities(city, orderBy, orderAs, page);
                return View(listCity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("CityManagement", Order = 2)]
        public ActionResult CityEdit(string cityId)
        {
            try
            {
                ViewData["operate"] = "Add";
                City city = new City();
                if (!string.IsNullOrWhiteSpace(cityId))
                {
                    ViewData["operate"] = "Update";
                    city = CityService.GetCityById(new Guid(cityId));
                }

                return View(city);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 页面事件

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string EditCity(City city, string operate)
        {
            try
            {
                if ("Update".Equals(operate))
                {
                    return CityService.UpdateCity(city) ? "OK" : "Error";
                }
                return CityService.InsertCity(city) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteCity(string cityId)
        {
            try
            {
                return CityService.DeleteCity(new Guid(cityId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
