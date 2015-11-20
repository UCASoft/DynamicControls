using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class InputControl : LabeledControl<InputControl>
    { 
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = new TagBuilder("input");
            SetValue(control);
            return control;
        }

        private void SetValue(TagBuilder control)
        {
            string value = Data.Value<string>("value");
            if (!string.IsNullOrEmpty(value))
                control.Attributes.Add("value", value);
        }
    }
}
