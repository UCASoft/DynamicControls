using System.Web;
using DynamicControls.Controls;
using Newtonsoft.Json.Linq;

namespace DynamicControls
{
    /// <summary>
    /// The dynamic controls builder.
    /// </summary>
    public class DynamicControlsBuilder : IHtmlString
    {
        /// <summary>
        /// The data source delegate key.
        /// </summary>
        public const string DataSourceDelegateKey = "DataSourceDelegate";

        /// <summary>
        /// The additional properties key.
        /// </summary>
        public const string AdditionalPropertiesKey = "AdditionalProperties";

        /// <summary>
        /// The area data key.
        /// </summary>
        private const string AreaDataKey = "AreaDataFor{0}";

        /// <summary>
        /// The area control.
        /// </summary>
        private readonly AreaControl areaControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicControlsBuilder"/> class.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        public DynamicControlsBuilder(string controls)
        {
            JObject control = JObject.Parse(controls);
            areaControl = AreaControl.Parse(control);
            HttpContext.Current.Session[GetAreaTempDataKey(areaControl.Name)] = control;
        }

        /// <summary>
        /// The get area temp data key.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAreaTempDataKey(string name)
        {
            return string.Format(AreaDataKey, name);
        }

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public string ToHtmlString()
        {
            return areaControl.Render();
        }

        /// <summary>
        /// The register data source delegate.
        /// </summary>
        /// <param name="dataSourceDelegate">
        /// The data source delegate.
        /// </param>
        /// <param name="additionalProperties">
        /// The additional properties.
        /// </param>
        /// <returns>
        /// The <see cref="DynamicControlsBuilder"/>.
        /// </returns>
        public DynamicControlsBuilder RegisterDataSourceDelegate(DataSourceDelegate dataSourceDelegate, params string[] additionalProperties)
        {
            HttpContext.Current.Session[DataSourceDelegateKey] = dataSourceDelegate;
            HttpContext.Current.Session[AdditionalPropertiesKey] = additionalProperties;
            return this;
        }
    }
}