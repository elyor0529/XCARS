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
    [System.Web.Http.RoutePrefix("AutoComfort")]
    public class AutoComfortController : BaseApiController
    {
        public IAutoComfortService AutoComfortService { get; set; }

        public AutoComfortController(IAutoComfortService autoComfortService)
        {
            AutoComfortService = autoComfortService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int[] selected)
        {
            return Ok(AutoComfortService.GetAllAsSelectList(selected));
        }
    }
}