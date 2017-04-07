using System.Linq;
using System.Web.Mvc;
using DynamicControls.Exceptions;
using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The parent control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class ValuedParentControl<T> : ValueControl<T>, IDynamicParentControl where T : BaseControl, new()
    {
        /// <summary>
        /// Gets a value indicating whether inner children.
        /// </summary>
        protected virtual bool InnerChildren
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether has childs.
        /// </summary>
        protected bool HasChilds
        {
            get { return Data.Value<JArray>("childs") != null; }
        }

        /// <summary>
        /// The get child by key.
        /// </summary>
        /// <param name="childs">
        /// The childs.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="JObject"/>.
        /// </returns>
        public static JObject GetChildByKey(JArray childs, string key)
        {
            return childs.FirstOrDefault(c => c.Value<string>("key").Contains(key + ',') || c.Value<string>("key").EndsWith(key)) as JObject;
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
        public virtual string RenderChilds(string parentValue)
        {
            string childsRender = string.Empty;
            JArray childs = Data.Value<JArray>("childs");
            if (childs != null)
            {
                childsRender += RenderAnyChilds(childs, parentValue);
                if (!string.IsNullOrEmpty(parentValue))
                {
                    childsRender += RenderChildsByValue(childs, parentValue);
                }
            }
            return childsRender;
        }

        /// <summary>
        /// The create child panel.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        internal static TagBuilder CreateChildPanel()
        {
            TagBuilder childPanel = new TagBuilder("div");
            childPanel.AddCssClass("child-panel");
            return childPanel;
        }

        /// <summary>
        /// The render any childs.
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
        protected internal static string RenderAnyChilds(JArray childs, string parentValue)
        {
            return RenderChildsByValue(childs, "any", parentValue);
        }

        /// <summary>
        /// The render childs by value.
        /// </summary>
        /// <param name="childs">
        /// The childs.
        /// </param>
        /// <param name="key">
        /// The key.
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
        protected internal static string RenderChildsByValue(JArray childs, string key, string parentValue)
        {
            string childsRender = string.Empty;
            JObject valueChilds = GetChildByKey(childs, key);
            if (valueChilds != null)
            {
                JArray controls = valueChilds.Value<JArray>("controls");
                if (controls != null)
                {
                    childsRender = controls.Values<JObject>().Aggregate(childsRender, (current, control) => current + CreateControl(control, parentValue).Render());
                }
                else
                {
                    throw new MissingControlsException();
                }
            }
            return childsRender;
        }

        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBodyAfterWorkControl(TagBuilder body)
        {
            if (HasChilds)
            {
                TagBuilder childPanel = CreateChildPanel();
                if (InnerChildren)
                {
                    childPanel.AddCssClass("inner-child");
                }
                childPanel.InnerHtml += RenderChilds(SendingValue);
                body.InnerHtml += childPanel.ToString();
            }
        }

        /// <summary>
        /// The create control.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="IDynamicRenderControl"/>.
        /// </returns>
        private static IDynamicRenderControl CreateControl(JObject control, string parentValue)
        {
            IDynamicRenderControl renderControl = CreateControl(control) as IDynamicRenderControl;
            IDynamicValueControl valueControl = renderControl as IDynamicValueControl;
            if (valueControl != null)
            {
                valueControl.SetParentValue(parentValue);
            }
            return renderControl;
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
        private string RenderChildsByValue(JArray childs, string parentValue)
        {
            return RenderChildsByValue(childs, parentValue, parentValue);
        }
    }
}
