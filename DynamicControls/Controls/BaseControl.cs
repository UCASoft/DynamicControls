using System;
using System.Web;

using DynamicControls.Delegates;

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
            string typeCode = control.Value<string>("type");
            string typeName = string.Format("DynamicControls.Controls.{0}Control", typeCode);
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                GetTypeDelegate getTypeDelegate = HttpContext.Current.Session[DynamicControlsBuilder.GetTypeDelegateKey] as GetTypeDelegate;
                if (getTypeDelegate != null)
                {
                    type = getTypeDelegate(typeCode);
                }
            }
            if (type == null)
            {
                throw new TypeLoadException();
            }
            IDynamicControl renderControl = Activator.CreateInstance(type) as IDynamicControl;
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
}