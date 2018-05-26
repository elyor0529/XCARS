using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Controllers.Apis
{
    [RoutePrefix("autostatistics")]
    public class AutoStatisticsController : BaseApiController
    {
        public IAutoStatisticsService AutoStatisticsService { get; set; }
        public IUserService UserService { get; set; }

        public AutoStatisticsController(IAutoStatisticsService autoStatisticsService, IUserService userService)
        {
            AutoStatisticsService = autoStatisticsService;
            UserService = userService;
        }

        [Route("GetNumberOfAutosGroupedByMake")]
        public IHttpActionResult GetNumberOfAutosGroupedByMake()
        {
            return Ok(AutoStatisticsService.GetNumberOfAutosGroupedByMake());
        }

        [Route("GetUserAutosNumberGroupedByStatus")]
        public IHttpActionResult GetUserAutosNumberGroupedByStatus()
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            return Ok(AutoStatisticsService.GetUserAutosNumberGroupedByStatus(user));
        }
    }
}