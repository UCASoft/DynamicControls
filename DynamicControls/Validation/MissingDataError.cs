namespace DynamicControls.Validation
{
    /// <summary>
    /// The missing data error.
    /// </summary>
    public class MissingDataError : ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingDataError"/> class.
        /// </summary>
        /// <param name="controlName">
        /// The control name.
        /// </param>
        public MissingDataError(string controlName) : base(string.Format("Didn't find data for control '{0}'!", controlName)) {}
    }
}
