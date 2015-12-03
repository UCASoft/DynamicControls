using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The html box control.
    /// </summary>
    public class HtmlBoxControl : BaseControl<HtmlBoxControl>
    {
        /// <summary>
        /// The create builder.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateBuilder()
        {
            TagBuilder controlBuilder = new TagBuilder("div");
            controlBuilder.AddCssClass("dynamic-control");
            controlBuilder.MergeAttribute("style", "text-align: center;");
            controlBuilder.InnerHtml += Data.Value<string>("html");
            return controlBuilder;
        }
    }
}
