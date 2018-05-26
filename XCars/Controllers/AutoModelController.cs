using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoModelController : Controller
    {
        public IAutoModelService AutoModelService { get; set; }

        public AutoModelController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoModelController(AutoModelService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectList(int makeID = 0, int selected = 0)
        {
            var ctrl = new Apis.AutoModelController(AutoModelService);
            var response = ctrl.GetAsSelectList(makeID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListMultiple(int[] makeID, int[] selected)
        {
            var ctrl = new Apis.AutoModelController(AutoModelService);
            var response = ctrl.GetAsSelectListMultiple(makeID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListWithParentIDMultiple(int[] makeID, int[] selected)
        {
            var ctrl = new Apis.AutoModelController(AutoModelService);
            var response = ctrl.GetAsSelectListWithParentIDMultiple(makeID, selected) as OkNegotiatedContentResult<List<object>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}