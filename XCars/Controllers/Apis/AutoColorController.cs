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
    [System.Web.Http.RoutePrefix("AutoColor")]
    public class AutoColorController : BaseApiController
    {
        public IAutoColorService AutoColorService { get; set; }

        public AutoColorController(IAutoColorService autoColorService)
        {
            AutoColorService = autoColorService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoColorService.GetAllAsSelectList(selected));
        }
    }
}