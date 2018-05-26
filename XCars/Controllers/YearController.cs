using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class YearController : Controller
    {
        public IYearService YearService { get; set; }

        public YearController()
        {
        }

        public ActionResult GetAllAsSelectList(int selected = 0)
        {
            var ctrl = new Apis.YearController(YearService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAsSelectListMultiple(int[] selected)
        {
            var ctrl = new Apis.YearController(YearService);
            var response = ctrl.GetAllAsSelectListMultiple(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}