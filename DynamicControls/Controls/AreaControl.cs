using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The area control.
    /// </summary>
    public class AreaControl : ParentControl<AreaControl>
    {
        /// <summary>
        /// The create parent builder.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateParentBuilder()
        {
            TagBuilder area = new TagBuilder("div");
            area.Attributes.Add("aria-dynamic", "true");
            return area;
        }
    }
}
