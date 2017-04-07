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

        /// <summary>
        /// The set default value.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        protected override void SetDefaultValue(TagBuilder control)
        {
            if (DefaultValue == "1")
            {
                control.Attributes.Add("checked", "checked");
            }
        }
    }
}
