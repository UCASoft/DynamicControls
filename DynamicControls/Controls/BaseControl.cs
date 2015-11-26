using System;
using System.Linq;
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
            if (HasChilds)
            {
                TagBuilder childPanel = CreateChildPanel();
                childPanel.InnerHtml += RenderChilds(this.DefaultValue);
                builder.InnerHtml += childPanel.ToString();
            }
            return builder.ToString();
        }

        /// <summary>
        /// The render childs.
        /// </summary>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string RenderChilds(string parentValue)
        {
            string childsRender = string.Empty;
            JArray childs = Data.Value<JArray>("childs");
            if (childs != null)
            {
                childsRender += RenderAnyChilds(childs);
                childsRender += RenderChildsByValue(childs, parentValue);
            }
            return childsRender;
        }

        /// <summary>
        /// The create builder.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected abstract TagBuilder CreateBuilder();

        /// <summary>
        /// The render any childs.
        /// </summary>
        /// <param name="childs">
        /// The childs.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RenderAnyChilds(JArray childs)
        {
            return RenderChildsByValue(childs, "any");
        }

        /// <summary>
        /// The render childs by value.
        /// </summary>
        /// <param name="childs">
        /// The childs.
        /// </param>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="MissingControlsException">
        /// The child object must have controls property.
        /// </exception>
        private static string RenderChildsByValue(JArray childs, string parentValue)
        {
            string childsRender = string.Empty;
            JObject valueChilds = childs.FirstOrDefault(c => c.Value<string>("key").Equals(parentValue)) as JObject;
            if (valueChilds != null)
            {
                JArray controls = valueChilds.Value<JArray>("controls");
                if (controls != null)
                {
                    childsRender = controls.Values<JObject>().Aggregate(childsRender, (current, control) => current + CreateControl(control).Render());
                }
                else
                    throw new MissingControlsException();
            }
            return childsRender;
        }

        /// <summary>
        /// The create control.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The <see cref="IDynamicRenderControl"/>.
        /// </returns>
        private static new IDynamicRenderControl CreateControl(JObject control)
        {
            return BaseControl.CreateControl(control) as IDynamicRenderControl;
        }

        /// <summary>
        /// The create child panel.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        private TagBuilder CreateChildPanel()
        {
            TagBuilder childPanel = new TagBuilder("div");
            childPanel.AddCssClass("child-panel");
            return childPanel;
        }
    }
}
