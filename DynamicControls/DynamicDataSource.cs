using System;
using System.Collections.Generic;

namespace DynamicControls
{
    public class DynamicDataSource : Dictionary<string, string>
    {
        public const string NullRowKey = "nullRow";

        public new void Add(string key, string value)
        {
            switch (key)
            {
                case NullRowKey:
                    throw new Exception();
                default:
                    base.Add(key, value);
                    break;
            }
        }
    }
}