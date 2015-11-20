using System.Web.Mvc;

namespace DynamicControls.Site.Controllers
{
    public class HomeController : DynamicController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}