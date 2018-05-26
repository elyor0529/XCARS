using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoComfortController : Controller
    {
        public IAutoComfortService AutoComfortService { get; set; }

        public AutoComfortController()
        {
        }

        public ActionResult GetAllAsSelectList(int[] selected)
        {
            var ctrl = new Apis.AutoComfortController(AutoComfortService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}