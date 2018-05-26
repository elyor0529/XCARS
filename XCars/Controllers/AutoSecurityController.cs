using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoSecurityController : Controller
    {
        public IAutoSecurityService AutoSecurityService { get; set; }

        public AutoSecurityController()
        {
        }

        public ActionResult GetAllAsSelectList(int[] selected)
        {
            var ctrl = new Apis.AutoSecurityController(AutoSecurityService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}