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
    public class MyAutoPhotoController : BaseApiController
    {
        public IUserService UserService { get; set; }
        public IAutoService AutoService { get; set; }
        public IAutoPhotoService AutoPhotoService { get; set; }

        public MyAutoPhotoController(IUserService userService, IAutoService autoService, IAutoPhotoService autoPhotoService)
        {
            UserService = userService;
            AutoService = autoService;
            AutoPhotoService = autoPhotoService;
        }

        [HttpPost]
        [Route("uploadphoto")]
        public IHttpActionResult UploadPhoto(int autoID, HttpPostedFileBase photo)
        {
            try
            {
                User user = UserService.GetUserByEmail(User.Identity.Name);
                Auto auto = AutoService.GetByID(autoID);
                if (auto == null || auto.UserID != user.ID)
                    return NotFound();

                int photoID = AutoPhotoService.UploadPhoto(autoID, photo);
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
                Auto auto = AutoPhotoService.GetByID(photoID).Auto;
                if (auto == null || auto.UserID != user.ID)
                    return NotFound();

                AutoPhotoService.MakePhotoMain(photoID);

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
                Auto auto = AutoService.GetByID(autoID);
                if (auto == null || auto.UserID != user.ID)
                    return NotFound();

                int mainPhotoID = AutoPhotoService.Delete(photoID);

                return Ok(mainPhotoID);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}