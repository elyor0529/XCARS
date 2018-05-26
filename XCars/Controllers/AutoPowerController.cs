using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoPowerController : Controller
    {
        public IAutoPowerService AutoPowerService { get; set; }

        public AutoPowerController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoPowerController(AutoPowerService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}