using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoFuelTypeController : Controller
    {
        public IAutoFuelTypeService AutoFuelTypeService { get; set; }

        public AutoFuelTypeController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoFuelTypeController(AutoFuelTypeService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAsSelectListMultiple(int[] selected)
        {
            var ctrl = new Apis.AutoFuelTypeController(AutoFuelTypeService);
            var response = ctrl.GetAllAsSelectListMultiple(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}