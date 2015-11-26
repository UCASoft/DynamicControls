using System.Web.Mvc;

namespace DynamicControls.Site.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : DynamicController
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}