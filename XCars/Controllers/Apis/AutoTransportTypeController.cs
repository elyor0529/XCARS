using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Service.Interfaces;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace XCars.Controllers.Apis
{
    [System.Web.Http.RoutePrefix("AutoTransportType")]
    public class AutoTransportTypeController : BaseApiController
    {
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }

        public AutoTransportTypeController(IAutoTransportTypeService autoTransportTypeService)
        {
            AutoTransportTypeService = autoTransportTypeService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoTransportTypeService.GetAllAsSelectList(selected));
        }
    }
}