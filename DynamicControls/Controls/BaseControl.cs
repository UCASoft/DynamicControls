using System;
using System.Linq;
using System.Web.Mvc;
using DynamicControls.Exceptions;
using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    public class BaseControl : IDynamicControl
    {
        protected JObject Data { get; private set; }

        protected internal string Name { get; private set; }

        protected bool HasChilds
        {
            get { return Data.Value<JArray>("childs") != null; }
        }

        protected string DefaulValue { get; private set; }

        public void Build(JObject control)
        {
            Data = control;
            Name = control.Value<string>("name");
            DefaulValue = control.Value<string>("defaultValue");
        }

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
    }

    public abstract class BaseControl<T> : BaseControl, IDynamicRenderControl where T : BaseControl, new()
    {
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

        public string Render()
        {
            TagBuilder builder = CreateBuilder();
            builder.GenerateId(Name);
            if (HasChilds)
            {
                TagBuilder childPanel = CreateChildPanel();
                childPanel.InnerHtml += RenderChilds(DefaulValue);
                builder.InnerHtml += childPanel.ToString();
            }
            return builder.ToString();
        }

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

        protected abstract TagBuilder CreateBuilder();

        private TagBuilder CreateChildPanel()
        {
            TagBuilder childPanel = new TagBuilder("div");
            childPanel.AddCssClass("child-panel");
            return childPanel;
        }

        private static string RenderAnyChilds(JArray childs)
        {
            return RenderChildsByValue(childs, "any");
        }

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

        private new static IDynamicRenderControl CreateControl(JObject control)
        {
            return BaseControl.CreateControl(control) as IDynamicRenderControl;
        }
    }
}
