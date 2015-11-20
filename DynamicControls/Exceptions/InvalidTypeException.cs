using System;

namespace DynamicControls.Exceptions
{
    public class InvalidTypeException : Exception {
        public InvalidTypeException(string type) : base(string.Format("Property type must by equals '{0}'!", type)) {}
    }
}
