using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The input control.
    /// </summary>
    public class InputControl : LabeledControl<InputControl>
    {
        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = new TagBuilder("input");
            control.Attributes.Add("onchange", "inputChange(this);");
            this.SetDefaultValue(control);
            return control;
        }

                /// <summary>
        /// The prepare checked roles.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="checkedRoles">
        /// The checked roles.
        /// </param>
        protected override void PrepareCheckedRoles(TagBuilder control, JObject checkedRoles)
        {
            if (checkedRoles.Value<bool>("required"))
            {
                control.Attributes.Add("required", "required");
            }
        }

        /// <summary>
        /// The set default value.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        protected override void SetDefaultValue(TagBuilder control)
        {
            string value = DefaultValue;
            if (!string.IsNullOrEmpty(value))
                control.Attributes.Add("value", value);
        }
    }
}
