using System.Web;
using System.Web.Mvc;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    /// <summary>
    /// The dynamic controller.
    /// </summary>
    public class DynamicController : Controller
    {
        /// <summary>
        /// The get childs.
        /// </summary>
        /// <param name="areaName">
        /// The area name.
        /// </param>
        /// <param name="parentName">
        /// The parent name.
        /// </param>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlString"/>.
        /// </returns>
        public HtmlString GetChilds(string areaName, string parentName, string parentValue)
        {
            JObject areaControl = Session[DynamicControlsBuilder.GetAreaTempDataKey(areaName)] as JObject;
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
