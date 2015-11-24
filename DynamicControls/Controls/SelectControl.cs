using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class SelectControl : DataSourceControl<SelectControl>
    {
        protected override TagBuilder CreateDataSourceControl()
        {
            var builder = new TagBuilder("select");
            if (HasChilds)
            {
                builder.Attributes.Add("onchange", "selectChange(this);");
            }
            return builder;
        }

        protected override void BindDataSource(TagBuilder control, DynamicDataSource dataSource)
        {
            foreach (KeyValuePair<string, string> pair in dataSource)
            {
                TagBuilder option = new TagBuilder("option");
                option.Attributes.Add("value", pair.Key);
                option.SetInnerText(pair.Value);
                control.InnerHtml += option.ToString();
            }
        }
    }
}
