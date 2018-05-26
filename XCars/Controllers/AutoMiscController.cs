using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoMiscController : Controller
    {
        public IAutoMiscService AutoMiscService { get; set; }

        public AutoMiscController()
        {
        }

        public ActionResult GetAllAsSelectList(int[] selected)
        {
            var ctrl = new Apis.AutoMiscController(AutoMiscService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}