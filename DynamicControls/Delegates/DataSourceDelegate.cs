namespace DynamicControls.Delegates
{
    using System.Collections.Generic;

    /// <summary>
    /// The data source delegate.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="additionalParameters">
    /// The additional parameters.
    /// </param>
    /// <returns>
    /// The data source object.
    /// </returns>
    public delegate DynamicDataSource DataSourceDelegate(string name, Dictionary<string, string> additionalParameters);
}