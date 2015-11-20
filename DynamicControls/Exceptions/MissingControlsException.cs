using System;

namespace DynamicControls.Exceptions
{
    public class MissingControlsException : MissingMemberException
    {
        public MissingControlsException() : base("Array 'controls' must be in 'child' property!") {}
    }
}