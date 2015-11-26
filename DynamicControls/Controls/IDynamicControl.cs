using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The DynamicControl interface.
    /// </summary>
    public interface IDynamicControl
    {
        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        void Build(JObject control);
    }
}