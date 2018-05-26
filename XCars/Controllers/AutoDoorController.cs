using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoDoorController : Controller
    {
        public IAutoDoorService AutoDoorService { get; set; }

        public AutoDoorController()
        {
        }

        public ActionResult GetAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.AutoDoorController(AutoDoorService);
            var response = ctrl.GetAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}