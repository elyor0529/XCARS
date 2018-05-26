using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        public IUserService UserService { get; set; }
        //public IPersonalDataService PersonalDataService { get; set; }
        public IRegionService RegionService { get; set; }
        public ICityService CityService { get; set; }
        public IFileManager FileManager { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public MyProfileController()
        {
            breadcrumbs.Add("/", Resource.Main);
            breadcrumbs.Add("/MyAuto", Resource.MyCabinet);
        }

        public ActionResult Edit()
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            if (user == null)
                return HttpNotFound();

            PersonalDataVM modelVM = user;
            //if ((modelVM == null || modelVM.ID == 0) && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            //    ViewBag.phoneNumber = user.PhoneNumber;

            breadcrumbs.Add("#", Resource.EditProfile);
            ViewBag.breadcrumbs = breadcrumbs;

            return View(modelVM);
        }

        [HttpPost]
        public ActionResult Edit(PersonalDataVM modelVM, HttpPostedFileBase photo, byte? photoChanged = null)
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    //PersonalData model;
                    //if (user?.PersonalData == null)
                    //{
                    //    model = new PersonalData();
                    //    model.PhotoUrl = $"{XCarsConfiguration.UserNoPhotoUrl}";
                    //    model.Users.Add(user);
                    //}
                    //else
                    //    model = user.PersonalData;


                    //user.Name = modelVM.Name;
                    user.FirstName = modelVM.FirstName;
                    user.LastName = modelVM.LastName;
                    user.RegionID = modelVM.RegionID;
                    user.CityID = modelVM.CityID;
                    user.PhoneNumber = modelVM.PhoneNumber;

                    if (photoChanged != null)
                    {
                        user.PhotoUrl = XCarsConfiguration.UserPhotosUploadUrl + XCarsConfiguration.UserNoPhotoName + XCarsConfiguration.PhotoExtension;
                        if (photo != null)
                            user.PhotoUrl = UserService.SaveUserPhotoAndGetUrl(user, photo);
                        else
                            UserService.DeleteUserPhotoFile(user);
                    }

                    //if (model.ID == 0)
                    //    PersonalDataService.CreatePD(model);
                    //else
                    //    PersonalDataService.EditPD(model);

                    UserService.EditUser(user);

                    //modelVM.PhotoSrc = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(FileManager.GetFile(model.PhotoUrl));
                    modelVM.PhotoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + user.PhotoUrl;

                    ViewBag.success = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", Resource.SaveError + ": " + ex.Message);
                }
            }
            else
                ModelState.AddModelError("", Resource.InvalidData);

            breadcrumbs.Add("#", Resource.EditProfile);
            ViewBag.breadcrumbs = breadcrumbs;
            
            return View(modelVM);
        }
    }
}