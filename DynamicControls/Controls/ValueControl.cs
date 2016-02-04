﻿using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The value control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class ValueControl<T> : BaseControl<T>, IDynamicValueControl where T : BaseControl, new()
    {
        /// <summary>
        /// Gets the default value.
        /// </summary>
        protected string DefaultValue { get; private set; }

        /// <summary>
        /// Gets or sets the sending value.
        /// </summary>
        protected string SendingValue { get; set; }

        /// <summary>
        /// Gets the parent value.
        /// </summary>
        protected string ParentValue { get; private set; }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        public override void Build(JObject control)
        {
            base.Build(control);
            DefaultValue = control.Value<string>("defaultValue");
            SendingValue = DefaultValue;
        }

        /// <summary>
        /// The set parent value.
        /// </summary>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        public void SetParentValue(string parentValue)
        {
            ParentValue = parentValue;
        }

        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBody(TagBuilder body)
        {
            body.MergeAttribute("value", DefaultValue);
            if (!string.IsNullOrEmpty(ParentValue))
                body.MergeAttribute("parent-value", ParentValue);
            PrepareBodyBeforeWorkControl(body);
            TagBuilder workControl = CreateControl();
            SetDefaultValue(workControl);
            if (Data["checkedRoles"] != null)
            {
                PrepareCheckedRoles(workControl, Data["checkedRoles"] as JObject);
            }
            workControl.AddCssClass("work-element");
            body.InnerHtml += workControl.ToString();
            PrepareBodyAfterWorkControl(body);            
        }

        /// <summary>
        /// The prepare body before work control.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected virtual void PrepareBodyBeforeWorkControl(TagBuilder body)
        {
        }

        /// <summary>
        /// The prepare value body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected virtual void PrepareBodyAfterWorkControl(TagBuilder body)
        {
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
        protected virtual void PrepareCheckedRoles(TagBuilder control, JObject checkedRoles)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected abstract TagBuilder CreateControl();

        /// <summary>
        /// The set default value.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        protected abstract void SetDefaultValue(TagBuilder control);
    }
}
