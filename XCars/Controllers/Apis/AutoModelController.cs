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
    [System.Web.Http.RoutePrefix("AutoModel")]
    public class AutoModelController : BaseApiController
    {
        public IAutoModelService AutoModelService { get; set; }

        public AutoModelController(IAutoModelService autoModelService)
        {
            AutoModelService = autoModelService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoModelService.GetAllAsSelectList(selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectList(int makeID = 0, int selected = 0)
        {
            return Ok(AutoModelService.GetAsSelectList(makeID, selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListMultiple")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectListMultiple(int[] makeID, int[] selected)
        {
            return Ok(AutoModelService.GetAsSelectListMultiple(makeID, selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListWithParentIDMultiple")]
        [ResponseType(typeof(List<object>))]
        public IHttpActionResult GetAsSelectListWithParentIDMultiple(int[] makeID, int[] selected)
        {
            return Ok(AutoModelService.GetAsSelectListWithParentIDMultiple(makeID, selected));
        }
    }
}