using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Controllers.Apis
{
    [RoutePrefix("myautophoto")]
    public class MyAuctionPhotoController : BaseApiController
    {
        public IUserService UserService { get; set; }
        public IAuctionService AuctionService { get; set; }
        public IAuctionPhotoService AuctionPhotoService { get; set; }

        public MyAuctionPhotoController(IUserService userService, IAuctionService auctionService, IAuctionPhotoService auctionPhotoService)
        {
            UserService = userService;
            AuctionService = auctionService;
            AuctionPhotoService = auctionPhotoService;
        }

        [HttpPost]
        [Route("uploadphoto")]
        public IHttpActionResult UploadPhoto(int auctionID, HttpPostedFileBase photo)
        {
            try
            {
                User user = UserService.GetUserByEmail(User.Identity.Name);
                Auction auction = AuctionService.GetByID(auctionID);
                if (auction == null || auction.Auto.UserID != user.ID)
                    return NotFound();

                int photoID = AuctionPhotoService.UploadPhoto(auctionID, photo);
                if (photoID == 0)
                    throw new Exception("Save error");

                return Ok(photoID);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("makephotomain")]
        public IHttpActionResult MakePhotoMain(int photoID)
        {
            try
            {
                User user = UserService.GetUserByEmail(User.Identity.Name);
                Auction auction = AuctionPhotoService.GetByID(photoID).Auction;
                if (auction == null || auction.Auto.UserID != user.ID)
                    return NotFound();

                AuctionPhotoService.MakePhotoMain(photoID);

                return Ok(photoID);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("deletephoto")]
        public IHttpActionResult DeletePhoto(int autoID, int photoID)
        {
            try
            {
                User user = UserService.GetUserByEmail(User.Identity.Name);
                Auction auction = AuctionService.GetByID(autoID);
                if (auction == null || auction.Auto.UserID != user.ID)
                    return NotFound();

                int mainPhotoID = AuctionPhotoService.Delete(photoID);

                return Ok(mainPhotoID);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}