using System.Web;
using System.Web.Mvc;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    public class DynamicController : Controller
    {
        public HtmlString GetChilds(string areaName, string parentName, string parentValue)
        {
            JObject areaControl = Session[DynamicControlsBuilder.GetAraeTempDataKey(areaName)] as JObject;
            if (areaControl != null)
            {
                JObject requestControl = areaControl.SelectToken(string.Format("$.childs..controls[?(@.name == '{0}')]", parentName)) as JObject;
                IDynamicRenderControl control = BaseControl.CreateControl(requestControl) as IDynamicRenderControl;
                if (control != null)
                    return new HtmlString(control.RenderChilds(parentValue));
            }
            return null;
        }
    }
}
