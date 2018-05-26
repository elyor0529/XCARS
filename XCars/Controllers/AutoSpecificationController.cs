using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Service;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    public class AutoSpecificationController : Controller
    {
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }
        public IAutoMakeService AutoMakeService { get; set; }
        public IAutoModelService AutoModelService { get; set; }

        public AutoSpecificationController()
        {
        }

        //[HttpPost]
        //public ActionResult GetAutoTransportTypesByMakeID(int MakeID = 0)
        //{
        //    var ctrl = new Apis.AutoSpecificationController(AutoTransportTypeService, AutoBodyTypeService, AutoMakeService, AutoModelService);
        //    var response = ctrl.GetAutoTransportTypesByMakeID(MakeID) as OkNegotiatedContentResult<List<SelectListItem>>;

        //    return Json(response.Content);
        //    //return Json(AutoTransportTypeService.GetByMakeIDAsSelectedList(MakeID));
        //}

        //[HttpPost]
        //public ActionResult GetAutoBodyTypesByTransportTypeID(int TransportTypeID = 0)
        //{
        //    var ctrl = new Apis.AutoSpecificationController(AutoTransportTypeService, AutoBodyTypeService, AutoMakeService, AutoModelService);
        //    var response = ctrl.GetAutoBodyTypesByTransportTypeID(TransportTypeID) as OkNegotiatedContentResult<List<SelectListItem>>;

        //    return Json(response.Content);
        //    //return Json(AutoBodyTypeService.GetByTransportTypeIDAsSelectList(TransportTypeID));
        //}

        //[HttpPost]
        //public ActionResult GetAutoMakesByTransportTypeIDAndBodyTypeID(int TransportTypeID = 0, int BodyTypeID = 0)
        //{
        //    var ctrl = new Apis.AutoSpecificationController(AutoTransportTypeService, AutoBodyTypeService, AutoMakeService, AutoModelService);
        //    var response = ctrl.GetAutoMakesByTransportTypeIDAndBodyTypeID(TransportTypeID, BodyTypeID) as OkNegotiatedContentResult<List<SelectListItem>>;

        //    return Json(response.Content);
        //    //return Json(AutoMakeService.GetByTransportTypeIDAndBodyTypeIDAsSelectList(TransportTypeID, BodyTypeID));
        //}

        //[HttpPost]
        //public ActionResult GetAutoModelsByTransportTypeIDAndBodyTypeIDAndMakeID(int TransportTypeID = 0, int BodyTypeID = 0, int MakeID = 0)
        //{
        //    var ctrl = new Apis.AutoSpecificationController(AutoTransportTypeService, AutoBodyTypeService, AutoMakeService, AutoModelService);
        //    var response = ctrl.GetAutoModelsByTransportTypeIDAndBodyTypeIDAndMakeID(TransportTypeID, BodyTypeID, MakeID) as OkNegotiatedContentResult<List<SelectListItem>>;

        //    return Json(response.Content);
        //    //return Json(AutoModelService.GetByTransportTypeIDAndBodyTypeIDAndMakeIDAsSelectList(TransportTypeID, BodyTypeID, MakeID));
        //}
    }
}