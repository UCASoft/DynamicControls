using System.Collections.Generic;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public class SelectControl : DataSourceControl<SelectControl>
    {
        protected override TagBuilder CreateControl()
        {
            var builder = new TagBuilder("select");
            if (HasChilds)
            {
                builder.Attributes.Add("onchange", "selectChange(this);");
            }
            BindDataSource(builder);
            return builder;
        }

        protected override void BindDataSource(TagBuilder builder)
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
