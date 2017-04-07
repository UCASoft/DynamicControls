using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    /// <summary>
    /// The select control.
    /// </summary>
    public class SelectControl : DataSourceControl<SelectControl>
    {
        /// <summary>
        /// Gets a value indicating whether null row.
        /// </summary>
        private bool NullRow
        {
            get { return Data.Value<bool>("nullRow"); }
        }

        /// <summary>
        /// The create data source control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateDataSourceControl()
        {
            var builder = new TagBuilder("select");
            builder.Attributes.Add("onchange", "selectChange(this);");
            return builder;
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
            if (NullRow)
                control.InnerHtml += RenderOption(DynamicDataSource.NullRowKey, string.Empty, false);
            foreach (KeyValuePair<string, string> pair in dataSource)            
                control.InnerHtml += RenderOption(pair.Key, pair.Value, defaultValue == pair.Key);
        }

        /// <summary>
        /// The render option.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="isSelected">
        /// Is option selected.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RenderOption(string key, string value, bool isSelected)
        {
            var option = new TagBuilder("option");
            option.Attributes.Add("value", key);
            if (isSelected)
            {
                option.Attributes.Add("selected", "selected");
            }
            option.SetInnerText(value);
            return option.ToString();
        }
    }
}
