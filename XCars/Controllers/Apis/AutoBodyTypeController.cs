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
    [System.Web.Http.RoutePrefix("AutoBodyType")]
    public class AutoBodyTypeController : BaseApiController
    {
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }

        public AutoBodyTypeController(IAutoBodyTypeService autoBodyTypeeService)
        {
            AutoBodyTypeService = autoBodyTypeeService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoBodyTypeService.GetAllAsSelectList(selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectList(int transportTypeID = 0, int selected = 0)
        {
            return Ok(AutoBodyTypeService.GetAsSelectList(transportTypeID, selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListMultiple")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectListMultiple(int[] transportTypeID, int[] selected)
        {
            return Ok(AutoBodyTypeService.GetAsSelectListMultiple(transportTypeID, selected));
        }
    }
}