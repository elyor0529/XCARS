using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Controllers.Apis
{
    [RoutePrefix("autospecification")]
    public class AutoSpecificationController : BaseApiController
    {
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }
        public IAutoMakeService AutoMakeService { get; set; }
        public IAutoModelService AutoModelService { get; set; }

        public AutoSpecificationController()
        {
        }

        //[HttpPost]
        //[Route("GetAutoTransportTypesByMakeID")]
        ////[ResponseType(typeof(List<CityVM>))]
        //public IHttpActionResult GetAutoTransportTypesByMakeID(int MakeID = 0)
        //{
        //    return Ok(AutoTransportTypeService.GetByMakeIDAsSelectedList(MakeID));
        //}

        //[HttpPost]
        //[Route("GetAutoBodyTypesByTransportTypeID")]
        //public IHttpActionResult GetAutoBodyTypesByTransportTypeID(int TransportTypeID = 0)
        //{
        //    return Ok(AutoBodyTypeService.GetByTransportTypeIDAsSelectList(TransportTypeID));
        //}

        //[HttpPost]
        //[Route("GetAutoMakesByTransportTypeIDAndBodyTypeID")]
        //public IHttpActionResult GetAutoMakesByTransportTypeIDAndBodyTypeID(int TransportTypeID = 0, int BodyTypeID = 0)
        //{
        //    return Ok(AutoMakeService.GetByTransportTypeIDAndBodyTypeIDAsSelectList(TransportTypeID, BodyTypeID));
        //}

        //[HttpPost]
        //[Route("GetAutoModelsByTransportTypeIDAndBodyTypeIDAndMakeID")]
        //public IHttpActionResult GetAutoModelsByTransportTypeIDAndBodyTypeIDAndMakeID(int TransportTypeID = 0, int BodyTypeID = 0, int MakeID = 0)
        //{
        //    return Ok(AutoModelService.GetByTransportTypeIDAndBodyTypeIDAndMakeIDAsSelectList(TransportTypeID, BodyTypeID, MakeID));
        //}
    }
}