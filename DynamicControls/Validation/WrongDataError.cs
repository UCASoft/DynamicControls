namespace DynamicControls.Validation
{
    /// <summary>
    /// The wrong data error.
    /// </summary>
    public class WrongDataError : ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongDataError"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public WrongDataError(string message) : base(message) { }
    }
}
