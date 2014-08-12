using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web
{
    public static class CheckBoxHelper
    {
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, bool isHorizon = true)
        {
            return SelectList(helper, name, name, helper.ViewData[name] as IEnumerable<SelectListItem>, new { }, isHorizon);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, object htmlAttributes, bool isHorizon = true)
        {
            return SelectList(helper, name, name, helper.ViewData[name] as IEnumerable<SelectListItem>, htmlAttributes, isHorizon);
        }

        public static MvcHtmlString SelectList(this HtmlHelper helper, string id, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isHorizon = true)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            HtmlAttributes.Add("id", id);
            HtmlAttributes.Add("name", name);

            string html = GetCheckBoxList(selectList);

            return MvcHtmlString.Create(html);
        }

        public static string GetCheckBoxList(IEnumerable<SelectListItem> selectList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SelectListItem select in selectList)
            {
                string checkbox = string.Format("<input type='checkbox' id='{0}' />", select.Value);
                string text = select.Text;
                sb.Append("<span>").Append(checkbox).Append(text).Append("</span>");
            }

            return sb.ToString();
        }
    }
}
