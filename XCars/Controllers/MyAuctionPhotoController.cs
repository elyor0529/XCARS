using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    [Authorize]
    public class MyAuctionPhotoController : Controller
    {
        public IUserService UserService { get; set; }
        public IAuctionService AuctionService { get; set; }
        public IAuctionPhotoService AuctionPhotoService { get; set; }

        public MyAuctionPhotoController()
        {
        }

        [HttpPost]
        public ActionResult UploadPhoto(int objectID, HttpPostedFileBase photo)
        {
            var ctrl = new Apis.MyAuctionPhotoController(UserService, AuctionService, AuctionPhotoService);
            var response = ctrl.UploadPhoto(objectID, photo) as OkNegotiatedContentResult<int>;
            if (response == null)
                return new HttpStatusCodeResult(500, "Save error");

            return Json(response.Content);
        }

        [HttpPost]
        public ActionResult MakePhotoMain(int photoID)
        {
            var ctrl = new Apis.MyAuctionPhotoController(UserService, AuctionService, AuctionPhotoService);
            var response = ctrl.MakePhotoMain(photoID) as OkNegotiatedContentResult<int>;
            if (response == null)
                return new HttpStatusCodeResult(500, "Save error");

            return Json(response.Content);
        }

        [HttpPost]
        public ActionResult DeletePhoto(int objectID, int photoID)
        {
            var ctrl = new Apis.MyAuctionPhotoController(UserService, AuctionService, AuctionPhotoService);
            var response = ctrl.DeletePhoto(objectID, photoID) as OkNegotiatedContentResult<int>;
            if (response == null)
                return new HttpStatusCodeResult(500, "Save error");

            return Json(response.Content);
        }
    }
}