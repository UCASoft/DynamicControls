using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DynamicControls.Delegates;

namespace DynamicControls.Controls
{   
    /// <summary>
    /// The data source control.
    /// </summary>
    /// <typeparam name="T">
    /// The type instance of BaseControl.
    /// </typeparam>
    public abstract class DataSourceControl<T> : LabeledControl<T> where T : BaseControl, new()
    {
        /// <summary>
        /// The create control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected override TagBuilder CreateControl()
        {
            TagBuilder control = CreateDataSourceControl();
            BindDataSource(control, GetDataSource(), DefaultValue);
            return control;
        }

        /// <summary>
        /// The create data source control.
        /// </summary>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        protected abstract TagBuilder CreateDataSourceControl();

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
        protected abstract void BindDataSource(TagBuilder control, DynamicDataSource dataSource, string defaultValue);

        /// <summary>
        /// The get data source.
        /// </summary>
        /// <returns>
        /// The <see cref="DynamicDataSource"/>.
        /// </returns>
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