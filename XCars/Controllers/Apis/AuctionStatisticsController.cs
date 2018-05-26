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
    [RoutePrefix("auctionstatistics")]
    public class AuctionStatisticsController : BaseApiController
    {
        public IAuctionStatisticsService AuctionStatisticsService { get; set; }
        public IUserService UserService { get; set; }

        public AuctionStatisticsController(IAuctionStatisticsService auctionStatisticsService, IUserService userService)
        {
            AuctionStatisticsService = auctionStatisticsService;
            UserService = userService;
        }

        //[Route("GetNumberOfAutosGroupedByMake")]
        //public IHttpActionResult GetNumberOfAutosGroupedByMake()
        //{
        //    return Ok(AutoStatisticsService.GetNumberOfAutosGroupedByMake());
        //}

        [Route("GetUserAutosNumberGroupedByStatus")]
        public IHttpActionResult GetUserAuctionsNumberGroupedByStatus()
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            return Ok(AuctionStatisticsService.GetUserAuctionsNumberGroupedByStatus(user));
        }
    }
}