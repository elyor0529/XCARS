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
    [System.Web.Http.RoutePrefix("AutoPower")]
    public class AutoPowerController : BaseApiController
    {
        public IAutoPowerService AutoPowerService { get; set; }

        public AutoPowerController(IAutoPowerService autoPowerService)
        {
            AutoPowerService = autoPowerService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoPowerService.GetAllAsSelectList(selected));
        }
    }
}