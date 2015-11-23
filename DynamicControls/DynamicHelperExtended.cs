using System.Web.Mvc;

namespace DynamicControls
{
    public static class DynamicHelperExtended
    {
        public static DynamicControlsBuilder DynamicControls(this HtmlHelper helper, string controls)
        {
            return new DynamicControlsBuilder(controls);
        }
    }
}
