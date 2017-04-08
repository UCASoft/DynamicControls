using System.Web.Mvc;

using DynamicControls.Exceptions;
using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The base control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of RenderControl.
    /// </typeparam>
    public abstract class RenderControl<T> : BaseControl, IDynamicRenderControl where T : BaseControl, new()
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
