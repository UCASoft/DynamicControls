using System.Web;
using System.Web.Mvc;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    public static class DynamicHelperExtended
    {
        public static string AreaTempDataKey = "TempAreaDataFor{0}";

        public static string GetAraeTempDataKey(string name)
        {
            return string.Format(AreaTempDataKey, name);
        }

        public static MvcHtmlString DynamicControls(this HtmlHelper helper, string controls)
        {
            JObject control = JObject.Parse(controls);
            AreaControl areaControl = AreaControl.Parse(control);
            HttpContext.Current.Session[GetAraeTempDataKey(areaControl.Name)] = control;
            return new MvcHtmlString(areaControl.Render());
        }
    }
}
