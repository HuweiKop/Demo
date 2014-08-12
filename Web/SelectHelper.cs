using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web
{
    public static class SelectHelper
    {
        public static MvcHtmlString SelectList(this HtmlHelper helper, string name, bool isHorizon = true)
        {
            return SelectList(helper, name, name, helper.ViewData[name] as IEnumerable<SelectListItem>, new { }, isHorizon);
        }

        public static MvcHtmlString SelectList(this HtmlHelper helper, string name, object htmlAttributes, bool isHorizon = true)
        {
            return SelectList(helper, name, name, helper.ViewData[name] as IEnumerable<SelectListItem>, htmlAttributes, isHorizon);
        }

        public static MvcHtmlString SelectList(this HtmlHelper helper, string id, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isHorizon = true)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            HtmlAttributes.Add("id", id);
            HtmlAttributes.Add("name", name);

            string optionString = GetOptionString(selectList);

            TagBuilder tagBuilder = new TagBuilder("select");
            tagBuilder.MergeAttributes<string, object>(HtmlAttributes);
            string selectHtml = tagBuilder.ToString(TagRenderMode.StartTag);
            string htmlString = selectHtml + optionString + "</select>";
            //string htmlString = string.Format("<select id=\"{0}\" name=\"{1}\">{2}</select>", id, name, optionString);

            return MvcHtmlString.Create(htmlString);
        }

        public static string GetOptionString(IEnumerable<SelectListItem> selectList)
        {
            string selectedValue = (selectList as SelectList).SelectedValue == null ? "" : Convert.ToString((selectList as SelectList).SelectedValue);

            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValue.Equals(item.Value) : selectedValue.Equals(item.Text);
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (SelectListItem selectItem in selectList)
            {
                string isSelected = "";
                if (selectItem.Selected) { isSelected = "selected='selected'"; }

                stringBuilder.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", selectItem.Value, selectItem.Text, isSelected);
            }

            return stringBuilder.ToString();
        }
    }
}
