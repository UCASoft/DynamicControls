using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace DynamicControls.Controls
{
    public abstract class DataSourceControl<T> : LabeledControl<T> where T : BaseControl, new()
    {
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = CreateDataSourceControl();
            BindDataSource(control, GetDataSource(), DefaulValue);
            return control;
        }

        protected abstract TagBuilder CreateDataSourceControl();

        protected abstract void BindDataSource(TagBuilder control, DynamicDataSource dataSource, string defaultValue);

        private DynamicDataSource GetDataSource()
        {
            DataSourceDelegate dataSourceDelegate = HttpContext.Current.Session[DynamicControlsBuilder.DataSourceDelegateKey] as DataSourceDelegate;
            if (dataSourceDelegate != null)
            {
                Dictionary<string, string> additionalParameters = new Dictionary<string, string>();
                string[] additionalProperties = HttpContext.Current.Session[DynamicControlsBuilder.AdditionalPropertiesKey] as string[];
                if (additionalProperties != null)
                {
                    foreach (string property in additionalProperties)
                    {
                        additionalParameters.Add(property, Data.Value<string>(property));
                    }
                }
                return dataSourceDelegate(Name, additionalParameters);
            }
            return null;
        }
    }
}