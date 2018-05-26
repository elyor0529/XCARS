using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoFuelConsumptionController : Controller
    {
        public IAutoFuelConsumptionService AutoFuelConsumptionService { get; set; }

        public AutoFuelConsumptionController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoFuelConsumptionController(AutoFuelConsumptionService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}