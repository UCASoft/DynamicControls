using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class DateControl : InputControl
    {
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = base.CreateControl();
            control.Attributes.Add("type", "date");
            if (HasChilds)
            {
                control.Attributes.Add("onchange", "dateChange(this);");
            }
            return control;
        }
    }
}
