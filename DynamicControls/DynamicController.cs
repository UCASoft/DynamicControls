using System.Web;
using System.Web.Mvc;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    /// <summary>
    /// The dynamic controller.
    /// </summary>
    public class DynamicController : Controller, IDynamicController
    {
        /// <summary>
        /// The get childs html.
        /// </summary>
        /// <param name="areaControl">
        /// The area control.
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
        public static HtmlString GetChildsHtml(JObject areaControl, string parentName, string parentValue)
        {
            if (areaControl != null)
            {
                JObject requestControl = areaControl.SelectToken(string.Format("$.childs..controls[?(@.name == '{0}')]", parentName)) as JObject;
                IDynamicParentControl control = BaseControl.CreateControl(requestControl) as IDynamicParentControl;
                if (control != null)
                    return new HtmlString(control.RenderChilds(parentValue));
            }
            return null;
        }

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
            return GetChildsHtml(areaControl, parentName, parentValue);
        }
    }
}
