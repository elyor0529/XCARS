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
    [System.Web.Http.RoutePrefix("AutoTransmissionType")]
    public class AutoTransmissionTypeController : BaseApiController
    {
        public IAutoTransmissionTypeService AutoTransmissionTypeService { get; set; }

        public AutoTransmissionTypeController(IAutoTransmissionTypeService autoTransmissionTypeService)
        {
            AutoTransmissionTypeService = autoTransmissionTypeService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectList")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectList(int selected = 0)
        {
            return Ok(AutoTransmissionTypeService.GetAllAsSelectList(selected));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllAsSelectListMultiple")]
        [ResponseType(typeof(List<SelectListItem>))]
        public IHttpActionResult GetAllAsSelectListMultiple(int[] selected)
        {
            return Ok(AutoTransmissionTypeService.GetAllAsSelectListMultiple(selected));
        }
    }
}