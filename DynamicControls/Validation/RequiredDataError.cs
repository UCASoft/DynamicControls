namespace DynamicControls.Validation
{
    /// <summary>
    /// The required data error.
    /// </summary>
    public class RequiredDataError : WrongDataError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredDataError"/> class.
        /// </summary>
        /// <param name="controlName">
        /// The control name.
        /// </param>
        public RequiredDataError(string controlName) : base(string.Format("Field '{0}' is required!", controlName)) { }
    }
}
