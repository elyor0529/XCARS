using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoStateController : Controller
    {
        public IAutoStateService AutoStateService { get; set; }

        public AutoStateController()
        {
        }

        public ActionResult GetAllAsSelectList(int[] selected)
        {
            var ctrl = new Apis.AutoStateController(AutoStateService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}