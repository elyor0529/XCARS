using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoMakeController : Controller
    {
        public IAutoMakeService AutoMakeService { get; set; }

        public AutoMakeController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoMakeController(AutoMakeService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoMakeController(AutoMakeService);
            var response = ctrl.GetAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListMultiple(int[] selected)
        {
            var ctrl = new Apis.AutoMakeController(AutoMakeService);
            var response = ctrl.GetAsSelectListMultiple(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}