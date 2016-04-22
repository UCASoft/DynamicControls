namespace DynamicControls.Validation
{
    /// <summary>
    /// The validation error.
    /// </summary>
    public class ValidationError 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ValidationError(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
    }
}
