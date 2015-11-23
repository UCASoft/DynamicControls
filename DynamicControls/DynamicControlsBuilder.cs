using System.Web;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    public class DynamicControlsBuilder : IHtmlString
    {
        public static string DataSourceDelegateKey = "DataSourceDelegate";

        public static string AdditionalPropertiesKey = "AdditionalProperties";

        private readonly AreaControl areaControl;

        private static string AreaDataKey = "AreaDataFor{0}";        

        public DynamicControlsBuilder(string controls)
        {
            JObject control = JObject.Parse(controls);
            areaControl = AreaControl.Parse(control);
            HttpContext.Current.Session[GetAraeTempDataKey(areaControl.Name)] = control;
        }

        public string ToHtmlString()
        {
            return areaControl.Render();
        }

        public DynamicControlsBuilder RegisterDataSourceDelegate(DataSourceDelegate dataSourceDelegate, params string[] additionalProperties)
        {
            HttpContext.Current.Session[DataSourceDelegateKey] = dataSourceDelegate;
            HttpContext.Current.Session["AdditionalProperties"] = additionalProperties;
            return this;
        }

        public static string GetAraeTempDataKey(string name)
        {
            return string.Format(AreaDataKey, name);
        }
    }
}