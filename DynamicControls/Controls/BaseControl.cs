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
        /// Gets a value indicating whether has childs.
        /// </summary>
        protected bool HasChilds
        {
            get { return Data.Value<JArray>("childs") != null; }
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        protected string DefaultValue { get; private set; }

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
        public void Build(JObject control)
        {
            Data = control;
            Name = control.Value<string>("name");
            DefaultValue = control.Value<string>("defaultValue");
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
            TagBuilder builder = CreateBuilder();
            builder.GenerateId(Name);

            return builder.ToString();
        }

        /// <summary>
        /// The create builder.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected abstract TagBuilder CreateBuilder();
    }
}
