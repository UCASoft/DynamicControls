using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The parent control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public class ParentControl<T> : RenderControl<T>, IDynamicParentControl where T : BaseControl, new()
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
                childsRender += ValuedParentControl<T>.RenderAnyChilds(childs, parentValue);
            }
            return childsRender;
        }

        /// <summary>
        /// The prepare body.
        /// </summary>
        /// <param name="body">
        /// The body.
        /// </param>
        protected override void PrepareBody(TagBuilder body)
        {
            if (HasChilds)
            {
                TagBuilder childPanel = ValuedParentControl<T>.CreateChildPanel();
                if (InnerChildren)
                {
                    childPanel.AddCssClass("inner-child");
                }
                childPanel.InnerHtml += RenderChilds(string.Empty);
                body.InnerHtml += childPanel.ToString();
            }
        }        
    }
}