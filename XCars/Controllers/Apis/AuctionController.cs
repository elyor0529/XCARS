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
    [RoutePrefix("auction")]
    public class AuctionController : BaseApiController
    {
        public IAuctionService AuctionService { get; set; }

        public AuctionController(IAuctionService auctionService)
        {
            AuctionService = auctionService;
        }

        //[HttpGet]
        [Route("GetAllActive")]
        //[ResponseType(typeof(List<CityVM>))]
        public IHttpActionResult GetAllActive()
        {
            return Ok(AuctionService.GetAllActive());
        }
    }
}