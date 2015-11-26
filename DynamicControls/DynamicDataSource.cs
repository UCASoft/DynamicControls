using System.Collections.Generic;

namespace DynamicControls
{
    using DynamicControls.Exceptions;

    /// <summary>
    /// The dynamic data source.
    /// </summary>
    public class DynamicDataSource : Dictionary<string, string>
    {
        /// <summary>
        /// The null row key.
        /// </summary>
        public const string NullRowKey = "nullRow";

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="UseLibraryKeyException">
        /// The try to use the library reserved key.
        /// </exception>
        public new void Add(string key, string value)
        {
            switch (key)
            {
                case NullRowKey:
                    throw new UseLibraryKeyException("NullRowKey");
                default:
                    base.Add(key, value);
                    break;
            }
        }
    }
}