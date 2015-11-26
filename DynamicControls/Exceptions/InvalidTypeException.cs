using System;

namespace DynamicControls.Exceptions
{
    /// <summary>
    /// The invalid type exception.
    /// </summary>
    public class InvalidTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTypeException"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public InvalidTypeException(string type) : base(string.Format("Property type must by equals '{0}'!", type)) {}
    }
}
