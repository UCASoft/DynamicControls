using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The area control.
    /// </summary>
    public class AreaControl : BaseControl<AreaControl>
    {
        /// <summary>
        /// The create builder.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateBuilder()
        {
            TagBuilder area = new TagBuilder("div");
            area.Attributes.Add("aria-dynamic", "true");
            return area;
        }
    }
}
