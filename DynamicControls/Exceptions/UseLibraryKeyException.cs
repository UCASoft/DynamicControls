namespace DynamicControls.Exceptions
{
    using System;

    /// <summary>
    /// The use library key exception.
    /// </summary>
    public class UseLibraryKeyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UseLibraryKeyException"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public UseLibraryKeyException(string key) : base(string.Format("You try to use the library reserved key '{0}'!", key))
        {
        }
    }
}