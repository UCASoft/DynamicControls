using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class SelectControl : LabeledControl<SelectControl>
    {
        protected override TagBuilder CreateControl()
        {
            return new TagBuilder("select");
        }
    }
}
