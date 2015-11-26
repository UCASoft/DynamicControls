namespace DynamicControls.Controls
{
    /// <summary>
    /// The DynamicRenderControl interface.
    /// </summary>
    public interface IDynamicRenderControl : IDynamicControl
    {
        /// <summary>
        /// The render.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string Render();

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