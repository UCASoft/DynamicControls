using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class CheckBoxControl : InputControl
    {
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = base.CreateControl();
            control.Attributes.Add("type", "checkbox");
            if (HasChilds)
            {
                control.Attributes.Add("onchange", "checkBoxChange(this);");
            }
            return control;
        }
    }
}
