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
            string value = DefaultValue;
            if (!string.IsNullOrEmpty(value))
            {
                control.Attributes.Add("value", value);
                control.Attributes.Add("text", value);
            }
        }
    }
}
