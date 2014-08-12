using BLL;
using BLLCommon;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace RET.Controllers
{
    public class PropertyController : Web.ControllerBase
    {
        //
        // GET: /Property/

        #region 页面加载

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("PropertyManagement", Order = 2)]
        public ActionResult PropertyList(Property property, int page, string orderBy, string orderAs)
        {
            try
            {
                Guid? userId = null;
                ViewData["Name"] = property.Name;
                PagedList<Property> listProperty = this.PropertyService.GetAllProperties(property, orderBy, orderAs, page);
                return View(listProperty);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("PropertyManagement", Order = 2)]
        public ActionResult PropertyEdit(string propertyId)
        {
            try
            {
                ViewData["operate"] = "Add";
                Property property = new Property();
                Guid? selectedCity = null;
                if (!string.IsNullOrWhiteSpace(propertyId))
                {
                    ViewData["operate"] = "Update";
                    property = PropertyService.GetPropertyById(new Guid(propertyId));
                    selectedCity = property.CityId;
                }

                var listCity = CommonModelService.GetAllData<City>();
                if (selectedCity == null && listCity.Count > 0)
                {
                    selectedCity = listCity[0].Id;
                }
                var listDistrict = CommonModelService.GetAllData<District>(obj => obj.CityId == selectedCity);
                this.ViewBag.CityId = new SelectList(listCity, "Id", "Name", selectedCity);
                this.ViewBag.DistrictId = new SelectList(listDistrict, "Id", "Name", property.DistrictId);

                return View(property);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [SessionTimeOutFilterAttribute(Order = 1)]
        [AuthorityFilterAttribute("PropertyManagement", Order = 2)]
        public ActionResult PropertyListDialog(Property property, int page)
        {
            try
            {
                PagedList<Property> listProperty = PropertyService.GetAllProperties(property, "", "", page);
                return View(listProperty);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region 页面事件

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string EditProperty(Property property, string operate)
        {
            try
            {
                if ("Update".Equals(operate))
                {
                    return PropertyService.UpdateProperty(property) ? "OK" : "Error";
                }
                if ("0".Equals((string)Session["IsAdmin"]))
                {
                    property.CreatedBy = (Guid)Session["UserId"];
                    property.CreatedOn = DateTime.Now;
                }
                return PropertyService.InsertProperty(property) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string DeleteProperty(string propertyId)
        {
            try
            {
                return PropertyService.DeleteProperty(new Guid(propertyId)) ? "OK" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        [SessionTimeOutFilterAttribute(Order = 1)]
        public string GetDistrictByCityId(string cityId)
        {
            try
            {
                var listDistrict = CommonModelService.GetAllData<District>(obj => obj.CityId == new Guid(cityId)).OrderBy(obj => obj.Name);
                var selectList = new SelectList(listDistrict, "Id", "Name", "");
                string optionStr = SelectHelper.GetOptionString(selectList as IEnumerable<SelectListItem>);

                return optionStr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
