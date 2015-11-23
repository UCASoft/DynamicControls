using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class SelectControl : DataSourceControl<SelectControl>
    {
        protected override TagBuilder CreateControl()
        {
            var builder = new TagBuilder("select");
            BuildDataSource(builder);
            return builder;
        }

        protected override void BuildDataSource(TagBuilder builder)
        {
            foreach (KeyValuePair<string, string> pair in GetDataSource())
            {
                TagBuilder option = new TagBuilder("option");
                option.Attributes.Add("value", pair.Key);
                option.SetInnerText(pair.Value);
                builder.InnerHtml += option.ToString();
            }
        }
    }
}
