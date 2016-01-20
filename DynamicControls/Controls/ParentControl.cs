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
    public abstract class ParentControl<T> : ValueControl<T>, IDynamicParentControl where T : BaseControl, new()
    {
        /// <summary>
        /// Gets a value indicating whether has childs.
        /// </summary>
        private bool HasChilds
        {
            get { return Data.Value<JArray>("childs") != null; }
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
                if (!string.IsNullOrEmpty(parentValue))
                    childsRender += RenderChildsByValue(childs, parentValue);
            }
            return childsRender;
        }

        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareValueBody(TagBuilder body)
        {
            if (HasChilds)
            {
                TagBuilder childPanel = CreateChildPanel();
                childPanel.InnerHtml += RenderChilds(DefaultValue);
                body.InnerHtml += childPanel.ToString();
            }
        }

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
            JObject valueChilds = childs.FirstOrDefault(c => c.Value<string>("key").Contains(parentValue + ',') || c.Value<string>("key").EndsWith(parentValue)) as JObject;
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