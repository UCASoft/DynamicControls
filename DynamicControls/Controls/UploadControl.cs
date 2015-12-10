using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The upload control.
    /// </summary>
    public class UploadControl : InputControl
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
            control.Attributes["type"] = "file";
            return control;
        }
    }
}
