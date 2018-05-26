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
    [System.Web.Http.RoutePrefix("AutoDoor")]
    public class AutoDoorController : BaseApiController
    {
        public IAutoDoorService AutoDoorService { get; set; }

        public AutoDoorController(IAutoDoorService autoDoorService)
        {
            AutoDoorService = autoDoorService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectList(int selected = 0)
        {
            return Ok(AutoDoorService.GetAsSelectList(selected));
        }
    }
}