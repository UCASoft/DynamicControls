using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The text area control.
    /// </summary>
    public class TextAreaControl : LabeledControl<TextAreaControl>
    {
        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = new TagBuilder("textarea");
            control.Attributes.Add("onchange", "textAreaChange(this);");
            JProperty rowsProperty = Data.Property("rows");
            if (rowsProperty != null)
            {
                control.Attributes.Add("rows", rowsProperty.Value.ToString());
            }
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
                control.SetInnerText(value);
            }
        }
    }
}
