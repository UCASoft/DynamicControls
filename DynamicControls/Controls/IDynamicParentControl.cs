namespace DynamicControls.Controls
{
    /// <summary>
    /// The DynamicParentControl interface.
    /// </summary>
    public interface IDynamicParentControl : IDynamicRenderControl
    {
        /// <summary>
        /// The render childs.
        /// </summary>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string RenderChilds(string parentValue);
    }
}
