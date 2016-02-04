using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The labeled control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class LabeledControl<T> : ValuedParentControl<T> where T : BaseControl, new()
    {
        /// <summary>
        /// The prepare body before work control.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBodyBeforeWorkControl(TagBuilder body)
        {
            body.InnerHtml += RenderLabel();
        }

        /// <summary>
        /// The prepare value body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBodyAfterWorkControl(TagBuilder body)
        {           
            body.InnerHtml += RenderText();
            base.PrepareBodyAfterWorkControl(body);
        }

        /// <summary>
        /// The render text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string RenderText()
        {
            string text = Data.Value<string>("text");
            if (!string.IsNullOrEmpty(text))
            {
                TagBuilder textBuilder = new TagBuilder("span");
                textBuilder.SetInnerText(text);
                return textBuilder.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// The render label.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string RenderLabel()
        {
            string label = Data.Value<string>("label");
            if (!string.IsNullOrEmpty(label))
            {
                TagBuilder labelBuilder = new TagBuilder("label");
                labelBuilder.AddCssClass("label-element");
                labelBuilder.InnerHtml += label;
                return labelBuilder.ToString();
            }
            return string.Empty;            
        }
    }
}
