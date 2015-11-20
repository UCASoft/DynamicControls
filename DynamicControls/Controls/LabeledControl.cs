using System;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public abstract class LabeledControl<T> : BaseControl<T> where T : BaseControl, new()
    {
        protected override TagBuilder CreateBuilder()
        {
            TagBuilder controlBuilder = new TagBuilder("div");
            controlBuilder.AddCssClass("dynamic-control");
            controlBuilder.InnerHtml += RenderLabel();
            var workBuilder = CreateControl();
            workBuilder.AddCssClass("work-element");
            controlBuilder.InnerHtml += workBuilder.ToString();
            controlBuilder.InnerHtml += RenderText();
            return controlBuilder;
        }

        private string RenderText()
        {
            string text = Data.Value<string>("text");
            if (!string.IsNullOrEmpty(text))
            {
                TagBuilder textBuilder = new TagBuilder("span");
                textBuilder.InnerHtml += text;
                return textBuilder.ToString();
            }
            return string.Empty;
        }

        protected abstract TagBuilder CreateControl();

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
