using System.Web;

namespace DynamicControls
{
    /// <summary>
    /// The DynamicController interface.
    /// </summary>
    public interface IDynamicController
    {
        /// <summary>
        /// The get childs.
        /// </summary>
        /// <param name="areaName">
        /// The area name.
        /// </param>
        /// <param name="parentName">
        /// The parent name.
        /// </param>
        /// <param name="parentValue">
        /// The parent value.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlString"/>.
        /// </returns>
        HtmlString GetChilds(string areaName, string parentName, string parentValue);
    }
}