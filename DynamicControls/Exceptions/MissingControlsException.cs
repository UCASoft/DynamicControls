using System;

namespace DynamicControls.Exceptions
{
    /// <summary>
    /// The missing controls exception.
    /// </summary>
    public class MissingControlsException : MissingMemberException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingControlsException"/> class.
        /// </summary>
        public MissingControlsException() : base("Array 'controls' must be in 'childs' property!") { }
    }
}