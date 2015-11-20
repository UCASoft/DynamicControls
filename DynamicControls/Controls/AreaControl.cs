using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class AreaControl : BaseControl<AreaControl>
    {
        protected override TagBuilder CreateBuilder()
        {
            TagBuilder area = new TagBuilder("div");
            area.Attributes.Add("aria-dynamic", "true");
            return area;
        }
    }
}
