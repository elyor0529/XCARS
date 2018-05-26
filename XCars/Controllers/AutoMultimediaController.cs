using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoMultimediaController : Controller
    {
        public IAutoMultimediaService AutoMultimediaService { get; set; }

        public AutoMultimediaController()
        {
        }

        public ActionResult GetAllAsSelectList(int[] selected)
        {
            var ctrl = new Apis.AutoMultimediaController(AutoMultimediaService);
            var response = ctrl.GetAllAsSelectList(selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}