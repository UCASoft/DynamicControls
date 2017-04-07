using System.Linq;
using System.Web;
using DynamicControls.Controls;
using DynamicControls.Delegates;
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
        /// The registered types.
        /// </summary>
        public const string GetTypeDelegateKey = "GetTypeDelegate";

        /// <summary>
        /// The area data key.
        /// </summary>
        private const string AreaDataKey = "AreaDataFor{0}";

        /// <summary>
        /// The controls.
        /// </summary>
        private readonly string controls;

        /// <summary>
        /// The data.
        /// </summary>
        private string data;

        /// <summary>
        /// The postfix.
        /// </summary>
        private string postfix;        

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicControlsBuilder"/> class.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        public DynamicControlsBuilder(string controls)
        {
            postfix = string.Empty;
            this.controls = controls;
        }

        /// <summary>
        /// Load input data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The <see cref="DynamicControlsBuilder"/>.
        /// </returns>
        public DynamicControlsBuilder LoadData(string data)
        {
            this.data = data;
            return this;
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
            JObject control = JObject.Parse(controls);
            ApplyPostfix(control);
            JObject inputData = JObject.Parse(data);
            ApplyData(control, inputData);
            AreaControl areaControl = AreaControl.Parse(control);
            HttpContext.Current.Session[GetAreaTempDataKey(areaControl.Name)] = control;
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

        /// <summary>
        /// The register get type delegate.
        /// </summary>
        /// <param name="getTypeDelegate">
        /// The get type delegate.
        /// </param>
        /// <returns>
        /// The <see cref="DynamicControlsBuilder"/>.
        /// </returns>
        public DynamicControlsBuilder RegisterGetTypeDelegate(GetTypeDelegate getTypeDelegate)
        {
            HttpContext.Current.Session[GetTypeDelegateKey] = getTypeDelegate;
            return this;
        }

        /// <summary>
        /// The set postfix.
        /// </summary>
        /// <param name="postfix">
        /// The postfix.
        /// </param>
        /// <returns>
        /// The <see cref="DynamicControlsBuilder"/>.
        /// </returns>
        public DynamicControlsBuilder SetPostfix(string postfix)
        {
            this.postfix = postfix;
            return this;
        }

        /// <summary>
        /// The apply postfix.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        private void ApplyPostfix(JObject control)
        {
            JToken nameToken = control["name"];
            if (nameToken != null)
            {
                nameToken.Replace(string.Format("{0}{1}", nameToken, postfix));
            }
            JArray childToken = control["childs"] as JArray;
            if (childToken != null)
            {
                foreach (JObject child in childToken.OfType<JObject>())
                {
                    JArray childControls = child["controls"] as JArray;
                    if (childControls != null)
                    {
                        foreach (JObject childControl in childControls.OfType<JObject>())
                        {
                            ApplyPostfix(childControl);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The apply data.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        private void ApplyData(JObject control, JObject data)
        {
            JProperty form = data.First as JProperty;
            if (form != null && control.Value<string>("name") == form.Name)
            {
                JObject innerData = form.Value as JObject;
                if (innerData != null)
                {
                    foreach (JProperty property in innerData.Properties())
                    {
                        JObject childControl = control.SelectToken(string.Format("$..*[?(@.name == '{0}')]", property.Name)) as JObject;
                        if (childControl != null)
                        {
                            JToken defaulValue = childControl["defaultValue"];
                            if (defaulValue == null)
                            {
                                defaulValue = new JProperty("defaultValue");
                                childControl.Add(defaulValue);
                            }
                            childControl["defaultValue"] = property.Value.Value<string>("value");
                        }
                    }
                }
            }
        }
    }
}