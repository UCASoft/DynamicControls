using System.Collections.Generic;
using System.Linq;

using DynamicControls.Controls;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Validation
{
    /// <summary>
    /// The dynamic validator.
    /// </summary>
    public class DynamicValidator
    {
        /// <summary>
        /// The validate data.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="errorList">
        /// The errorList.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ValidateData(JObject controls, JObject data, List<ValidationError> errorList)
        {
            errorList.AddRange(ValidateData(controls, data));
            return errorList.Count == 0;
        }

        /// <summary>
        /// The validate data.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see><cref>List</cref></see>.
        /// </returns>
        public static List<ValidationError> ValidateData(JObject controls, JObject data)
        {
            List<ValidationError> errorList = new List<ValidationError>();
            JObject checkedData = JObject.Parse(data.ToString());
            ValidateValues(controls.Value<JArray>("childs").First().Value<JArray>("controls"), checkedData, errorList);
            IEnumerable<JProperty> excessDatas = checkedData.OfType<JProperty>().Where(p => !p.First.Value<bool>("checked"));
            errorList.AddRange(excessDatas.Select(excessData => new ExcessDataError(excessData.Name)));
            return errorList;
        }

        /// <summary>
        /// The validate values.
        /// </summary>
        /// <param name="controls">
        /// The controls.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="errorList">
        /// The errorList.
        /// </param>
        private static void ValidateValues(JArray controls, JObject data, List<ValidationError> errorList)
        {
            foreach (JObject child in controls.OfType<JObject>())
            {
                IDynamicControl control = BaseControl.CreateControl(child);
                IDynamicValueControl valueControl = control as IDynamicValueControl;
                JProperty dataValue = null;
                if (valueControl != null)
                {
                    dataValue = data.Children<JProperty>().FirstOrDefault(p => p.Name == child.Value<string>("name"));
                    if (dataValue != null)
                    {
                        JObject dataObject = dataValue.First as JObject;
                        if (dataObject != null)
                        {
                            CheckRoles(dataObject, child, errorList);
                            dataObject.Add(new JProperty("checked", true));
                        }
                    }
                    else
                    {
                        errorList.Add(new MissingDataError(child.Value<string>("name")));
                    }
                }
                IDynamicParentControl parentControl = control as IDynamicParentControl;
                if (parentControl != null)
                {
                    JArray children = child.Value<JArray>("childs");
                    if (children != null)
                    {
                        JObject anyChildren = children.OfType<JObject>().FirstOrDefault(c => c.Value<string>("key") == "any");
                        if (anyChildren != null)
                        {
                            ValidateValues(anyChildren.Value<JArray>("controls"), data, errorList);
                        }
                        if (valueControl != null && dataValue != null)
                        {
                            JObject valueChildren = children.OfType<JObject>().FirstOrDefault(c => c.Value<string>("key") == dataValue.First.Value<string>("value"));
                            if (valueChildren != null)
                            {
                                ValidateValues(valueChildren.Value<JArray>("controls"), data, errorList);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The check roles.
        /// </summary>
        /// <param name="dataObject">
        /// The data object.
        /// </param>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        private static void CheckRoles(JToken dataObject, JObject control, ICollection<ValidationError> errorList)
        {
            JObject roles = control["checkedRoles"] as JObject;
            if (roles != null)
            {
                if (roles.Value<bool>("required") && string.IsNullOrEmpty(dataObject.Value<string>("value")))
                {
                    errorList.Add(new RequiredDataError(control.Value<string>("name")));
                }
            }
        }
    }
}