using System.Collections.Generic;
using System.Web.Mvc;

using DynamicControls.Validation;

using Newtonsoft.Json.Linq;

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

        /// <summary>
        /// The send data.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult SendData(string data)
        {
            List<ValidationError> result = new List<ValidationError>();
            JObject parseData = JObject.Parse(data);
            DynamicValidator.ValidateData(Session[DynamicControlsBuilder.GetAreaTempDataKey(parseData.Value<string>("area-name"))] as JObject, parseData, result);
            return Json(result);
        }
    }
}