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
    [RoutePrefix("myauction")]
    public class MyAuctionController : BaseApiController
    {
        public IAuctionService AuctionService { get; set; }
        public IUserService UserService { get; set; }
        public IAuctionFavoriteService AuctionFavoriteService { get; set; }

        public MyAuctionController(IAuctionService auctionService, IUserService userService, IAuctionFavoriteService auctionFavoriteService)
        {
            AuctionService = auctionService;
            UserService = userService;
            AuctionFavoriteService = auctionFavoriteService;
        }

        [HttpPost]
        [Route("AddToFavorites")]
        //[ResponseType(typeof(Model.City))]
        public IHttpActionResult AddToFavorites(int id)
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);

            Auction auto = AuctionService.GetByID(id);
            if (auto == null)
                return NotFound();

            try
            {
                int result = 1;
                AuctionFavorite fav = user.AuctionFavorites.FirstOrDefault(f => f.AuctionID == id);
                if (fav == null)
                {
                    user.AuctionFavorites.Add(new AuctionFavorite() { AuctionID = id });
                    UserService.EditUser(user);
                }
                else
                {
                    AuctionFavoriteService.Delete(fav);
                    result = -1;
                }

                AuctionService.Edit(auto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}