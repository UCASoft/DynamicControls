using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class SelectControl : DataSourceControl<SelectControl>
    {
        private bool NullRow
        {
            get { return Data.Value<bool>("nullRow"); }
        }

        protected override TagBuilder CreateDataSourceControl()
        {
            var builder = new TagBuilder("select");
            if (HasChilds)
            {
                builder.Attributes.Add("onchange", "selectChange(this);");
            }
            return builder;
        }

        protected override void BindDataSource(TagBuilder control, DynamicDataSource dataSource, string defaultValue)
        {
            if (NullRow)
                control.InnerHtml += RenderOption(DynamicDataSource.NullRowKey, string.Empty);
            foreach (KeyValuePair<string, string> pair in dataSource)            
                control.InnerHtml += RenderOption(pair.Key, pair.Value);
        }

        private static string RenderOption(string key, string value)
        {
            var option = new TagBuilder("option");
            option.Attributes.Add("value", key);
            option.SetInnerText(value);
            return option.ToString();
        }
    }
}
