﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The radio list control.
    /// </summary>
    public class RadioListControl : DataSourceControl<RadioListControl>
    {
        /// <summary>
        /// The create data source control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateDataSourceControl()
        {
            TagBuilder control = new TagBuilder("div");
            control.AddCssClass("radio-list");
            return control;
        }

        /// <summary>
        /// The bind data source.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="dataSource">
        /// The data source.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        protected override void BindDataSource(TagBuilder control, DynamicDataSource dataSource, string defaultValue)
        {
            KeyValuePair<string, string> lastPair = dataSource.Last();
            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "radio");
                input.Attributes.Add("name", Name);
                input.Attributes.Add("value", pair.Key);
                if (pair.Key.Equals(defaultValue))
                {
                    input.Attributes.Add("checked", "checked");
                }
                control.InnerHtml += input.ToString();
                TagBuilder span = new TagBuilder("span");
                span.SetInnerText(pair.Value);
                control.InnerHtml += span.ToString();
                if (!pair.Equals(lastPair))
                {
                    control.InnerHtml += "<br/>";
                }
            }
        }

        /// <summary>
        /// The prepare checked roles.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="checkedRoles">
        /// The checked roles.
        /// </param>
        protected override void PrepareCheckedRoles(TagBuilder control, JObject checkedRoles)
        {
            if (checkedRoles.Value<bool>("required"))
            {
                Regex regex = new Regex(Regex.Escape("></input>"));
                control.InnerHtml = regex.Replace(control.InnerHtml, " required=\"required\"></input>", 1);
            }
        }
    }
}
