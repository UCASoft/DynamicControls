using System;
using System.Web.Mvc;
using DynamicControls.Exceptions;
using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The base control.
    /// </summary>
    public class BaseControl : IDynamicControl
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        protected internal string Name { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        protected JObject Data { get; private set; }

        /// <summary>
        /// The create control.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The <see cref="IDynamicControl"/>.
        /// </returns>
        public static IDynamicControl CreateControl(JObject control)
        {
            string typeName = string.Format("DynamicControls.Controls.{0}Control", control.Value<string>("type"));
            IDynamicControl renderControl = Activator.CreateInstance(Type.GetType(typeName, true)) as IDynamicControl;
            if (renderControl != null)
            {
                renderControl.Build(control);
                return renderControl;
            }
            return null;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        public virtual void Build(JObject control)
        {
            Data = control;
            Name = control.Value<string>("name");
        }
    }

    /// <summary>
    /// The base control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class BaseControl<T> : BaseControl, IDynamicRenderControl where T : BaseControl, new()
    {
        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="InvalidInputData">
        /// The input data was invalid.
        /// </exception>
        public static T Parse(JObject control)
        {
            if (control != null)
            {
                T result = new T();
                result.Build(control);
                return result;
            }
            throw new InvalidInputData();
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Render()
        {
            TagBuilder body = new TagBuilder("div");
            body.GenerateId(Name);
            body.AddCssClass("dynamic-control");
            PrepareBody(body);
            return body.ToString();
        }

        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected abstract void PrepareBody(TagBuilder body);
    }
}
