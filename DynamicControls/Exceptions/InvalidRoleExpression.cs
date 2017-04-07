using System;

namespace DynamicControls.Exceptions
{
    /// <summary>
    /// The invalid role expression.
    /// </summary>
    public class InvalidRoleExpression : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRoleExpression"/> class.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        public InvalidRoleExpression(string expression): base(string.Format("Expression '{0}' is invalid!", expression)) { }
    }
}