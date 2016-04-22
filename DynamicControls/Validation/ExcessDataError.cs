namespace DynamicControls.Validation
{
    /// <summary>
    /// The excess data error.
    /// </summary>
    public class ExcessDataError : ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcessDataError"/> class.
        /// </summary>
        /// <param name="dataFieldName">
        /// The data field name.
        /// </param>
        public ExcessDataError(string dataFieldName) : base(string.Format("The control '{0}' isn't described!", dataFieldName)) {}
    }
}
