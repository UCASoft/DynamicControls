using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The value control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class ValueControl<T> : BaseControl<T> where T : BaseControl, new()
    {
        /// <summary>
        /// Gets the default value.
        /// </summary>
        protected string DefaultValue { get; private set; }

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
            PrepareValueBody(body);            
        }

        /// <summary>
        /// The prepare value body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected abstract void PrepareValueBody(TagBuilder body);
    }
}
