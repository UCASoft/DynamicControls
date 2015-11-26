using System.Web.Mvc;

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
            SetValue(control);
            return control;
        }

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        private void SetValue(TagBuilder control)
        {
            string value = DefaultValue;
            if (!string.IsNullOrEmpty(value))
                control.Attributes.Add("value", value);
        }
    }
}
