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
                    CheckControl(dataValue, child, errorList);
                }
                IDynamicParentControl parentControl = control as IDynamicParentControl;
                if (parentControl != null)
                {
                    CheckChildren(child.Value<JArray>("childs"), dataValue, data, errorList);
                }
            }
        }

        /// <summary>
        /// The check control.
        /// </summary>
        /// <param name="dataValue">
        /// The data value.
        /// </param>
        /// <param name="child">
        /// The child.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        private static void CheckControl(JProperty dataValue, JObject child, ICollection<ValidationError> errorList)
        {
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

        /// <summary>
        /// The check children.
        /// </summary>
        /// <param name="children">
        /// The children.
        /// </param>
        /// <param name="dataValue">
        /// The data value.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        private static void CheckChildren(JArray children, JProperty dataValue, JObject data, List<ValidationError> errorList)
        {
            if (children != null)
            {
                CheckAnyChildren(children, data, errorList);
                CheckChildrenByValue(children, dataValue, data, errorList);
            }
        }

        /// <summary>
        /// The check any children.
        /// </summary>
        /// <param name="children">
        /// The children.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        private static void CheckAnyChildren(JArray children, JObject data, List<ValidationError> errorList)
        {
            JObject anyChildren = children.OfType<JObject>().FirstOrDefault(c => c.Value<string>("key") == "any");
            if (anyChildren != null)
            {
                ValidateValues(anyChildren.Value<JArray>("controls"), data, errorList);
            }
        }

        /// <summary>
        /// The check children by value.
        /// </summary>
        /// <param name="children">
        /// The children.
        /// </param>
        /// <param name="dataValue">
        /// The data value.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="errorList">
        /// The error list.
        /// </param>
        private static void CheckChildrenByValue(JArray children, JProperty dataValue, JObject data, List<ValidationError> errorList)
        {
            if (dataValue != null)
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