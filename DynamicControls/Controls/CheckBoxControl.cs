using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The check box control.
    /// </summary>
    public class CheckBoxControl : InputControl
    {
        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = base.CreateControl();
            control.Attributes.Add("type", "checkbox");
            control.Attributes["onchange"] = "checkBoxChange(this);";
            return control;
        }
    }
}
