using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RET.Controllers
{
    public class DistrictController : Web.ControllerBase
    {
        //
        // GET: /District/

        #region 页面加载

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("DistrictManagement", Order = 2)]
        public ActionResult DistrictList(District district, int page, string orderBy, string orderAs)
        {
            try
            {
                ViewData["Name"] = district.Name;
                PagedList<District> listDistrict = this.DistrictService.GetAllDistricts(district, orderBy, orderAs, page);
                return View(listDistrict);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("DistrictManagement", Order = 2)]
        public ActionResult DistrictEdit(string districtId)
        {
            try
            {
                ViewData["operate"] = "Add";
                District district = new District();
                if (!string.IsNullOrWhiteSpace(districtId))
                {
                    ViewData["operate"] = "Update";
                    district = DistrictService.GetDistrictById(new Guid(districtId));
                }

                var listCtiy = CommonModelService.GetAllData<City>();

                this.ViewBag.CityId = new SelectList(listCtiy, "Id", "Name", district.CityId);

                return View(district);
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
        public string EditDistrict(District district, string operate)
        {
            try
            {
                if ("Update".Equals(operate))
                {
                    return DistrictService.UpdateDistrict(district) ? "OK" : "Error";
                }
                return DistrictService.InsertDistrict(district) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteDistrict(string districtId)
        {
            try
            {
                return DistrictService.DeleteDistrict(new Guid(districtId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
