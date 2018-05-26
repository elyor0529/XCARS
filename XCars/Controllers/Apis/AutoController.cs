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
    [RoutePrefix("auto")]
    public class AutoController : BaseApiController
    {
        public IAutoService AutoService { get; set; }

        public AutoController(IAutoService autoService)
        {
            AutoService = autoService;
        }

        //[HttpGet]
        [Route("GetAllActive")]
        //[ResponseType(typeof(List<CityVM>))]
        public IHttpActionResult GetAllActive()
        {
            return Ok(AutoService.GetAllActive());
        }
    }
}