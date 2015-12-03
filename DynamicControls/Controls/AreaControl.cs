using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The area control.
    /// </summary>
    public class AreaControl : ParentControl<AreaControl>
    {
        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareValueBody(TagBuilder body)
        {
            body.Attributes.Add("aria-dynamic", "true");
            base.PrepareValueBody(body);
        }
    }
}
