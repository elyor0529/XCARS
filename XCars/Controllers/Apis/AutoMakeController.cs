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
    [System.Web.Http.RoutePrefix("AutoMake")]
    public class AutoMakeController : BaseApiController
    {
        public IAutoMakeService AutoMakeService { get; set; }

        public AutoMakeController(IAutoMakeService autoMakeService)
        {
            AutoMakeService = autoMakeService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoMakeService.GetAllAsSelectList(selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectList(int selected = 0)
        {
            return Ok(AutoMakeService.GetAsSelectList(selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListMultiple")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectListMultiple(int[] selected)
        {
            return Ok(AutoMakeService.GetAsSelectListMultiple(selected));
        }
    }
}