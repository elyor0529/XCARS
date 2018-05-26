using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoBodyTypeController : Controller
    {
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }

        public AutoBodyTypeController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoBodyTypeController(AutoBodyTypeService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectList(int transportTypeID = 0, int selected = 0)
        {
            var ctrl = new Apis.AutoBodyTypeController(AutoBodyTypeService);
            var response = ctrl.GetAsSelectList(transportTypeID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListMultiple(int[] transportTypeID, int[] selected)
        {
            var ctrl = new Apis.AutoBodyTypeController(AutoBodyTypeService);
            var response = ctrl.GetAsSelectListMultiple(transportTypeID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}