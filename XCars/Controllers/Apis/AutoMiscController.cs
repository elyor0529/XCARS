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
    [System.Web.Http.RoutePrefix("AutoMisc")]
    public class AutoMiscController : BaseApiController
    {
        public IAutoMiscService AutoMiscService { get; set; }

        public AutoMiscController(IAutoMiscService autoMiscService)
        {
            AutoMiscService = autoMiscService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int[] selected)
        {
            return Ok(AutoMiscService.GetAllAsSelectList(selected));
        }
    }
}