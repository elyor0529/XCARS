using System.Web.Http.Results;
using System.Web.Mvc;
using System.Linq;
using XCars.Service.Interfaces;
using System.Collections.Generic;
using XCars.ViewModels;

namespace XCars.Controllers
{
    public class CityController : Controller
    {
        public ICityService CityService { get; set; }

        public CityController()
        {
        }

        public ActionResult GetAsSelectList(int regionID = 0, int selected = 0)
        {
            var ctrl = new Apis.CityController(CityService);
            var response = ctrl.GetAsSelectList(regionID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListMultiple(int[] regionID, int[] selected)
        {
            var ctrl = new Apis.CityController(CityService);
            var response = ctrl.GetAsSelectListMultiple(regionID, selected) as OkNegotiatedContentResult<List<SelectListItem>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAsSelectListWithParentIDMultiple(int[] regionID, int[] selected)
        {
            var ctrl = new Apis.CityController(CityService);
            var response = ctrl.GetAsSelectListWithParentIDMultiple(regionID, selected) as OkNegotiatedContentResult<List<object>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}