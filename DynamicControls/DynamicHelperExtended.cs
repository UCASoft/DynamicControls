using System.Web.Mvc;

namespace DynamicControls
{
    /// <summary>
    /// The dynamic helper extended.
    /// </summary>
    public static class DynamicHelperExtended
    {
        /// <summary>
        /// The dynamic controls.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="controls">
        /// The controls.
        /// </param>
        /// <returns>
        /// The <see cref="DynamicControlsBuilder"/>.
        /// </returns>
        public static DynamicControlsBuilder DynamicControls(this HtmlHelper helper, string controls)
        {
            return new DynamicControlsBuilder(controls);
        }
    }
}
