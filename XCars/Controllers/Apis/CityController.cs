using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Model;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers.Apis
{
    [System.Web.Http.RoutePrefix("City")]
    public class CityController : BaseApiController
    {
        public ICityService CityService { get; set; }

        public CityController(ICityService cityService)
        {
            CityService = cityService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectList(int regionID = 0, int selected = 0)
        {
            return Ok(CityService.GetAsSelectList(regionID, selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListMultiple")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAsSelectListMultiple(int[] regionID, int[] selected)
        {
            return Ok(CityService.GetAsSelectListMultiple(regionID, selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAsSelectListWithParentIDMultiple")]
        [ResponseType(typeof(List<object>))]
        public IHttpActionResult GetAsSelectListWithParentIDMultiple(int[] regionID, int[] selected)
        {
            return Ok(CityService.GetAsSelectListWithParentIDMultiple(regionID, selected));
        }
    }
}
