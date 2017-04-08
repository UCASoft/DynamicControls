using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The html box control.
    /// </summary>
    public class HtmlBoxControl : RenderControl<HtmlBoxControl>
    {
        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBody(TagBuilder body)
        {
            body.MergeAttribute("style", "text-align: center;");
            body.InnerHtml += Data.Value<string>("html");
        }
    }
}
