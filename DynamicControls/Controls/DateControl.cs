using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The date control.
    /// </summary>
    public class DateControl : InputControl
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
            control.Attributes.Add("type", "date");
            if (HasChilds)
            {
                control.Attributes.Add("onchange", "dateChange(this);");
            }
            return control;
        }
    }
}
