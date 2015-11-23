using System.Collections.Generic;

namespace DynamicControls
{
    public delegate DynamicDataSource DataSourceDelegate(string name, Dictionary<string, string> additionalParameters);
}