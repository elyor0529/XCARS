using System;
using System.Web.Mvc;
using System.Linq;
using XCars.Model;
using XCars.Service.Interfaces;
using XCars.ViewModels;
using XCars.Common;
using XCars.Resourses;
using System.Collections.Generic;
using System.Web.Http.Results;
//using XCars.Service;

namespace XCars.Controllers
{
    public class AuctionController : Controller
    {
        public IUserService UserService { get; set; }
        public IAutoService AutoService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IAuctionService AuctionService { get; set; }
        public ISiteSettingsService SiteSettingsService { get; set; }
        public IFileManager FileManager { get; set; }
        public IScheduledEmailService ScheduledEmailService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public AuctionController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: Auction
        public ActionResult Index()
        {
            //int userID = 0;
            //if (User.Identity.IsAuthenticated)
            //{
            //    User user = UserService.GetUserByEmail(User.Identity.Name);
            //    if (user != null)
            //        userID = user.ID;
            //}
            //ViewBag.userID = userID;

            breadcrumbs.Add("#", Resource.Auctions);
            ViewBag.breadcrumbs = breadcrumbs;

            ExtendedSearchVM modelVM = new ExtendedSearchVM()
            {
                StatusID = 2,
                Type = "auction"
            };

            return View("~/Views/SearchAuto/SearchResult.cshtml", modelVM);
        }

        //[Authorize]
        public ActionResult Details(int id)
        {
            Auction auction = AuctionService.GetByID(id); //should be just GetByID(id)
            if (auction == null)
                return HttpNotFound();

            auction.Views++;
            AuctionService.Edit(auction);

            int userID = 0;
            bool currentUserIsOwner = false;
            bool isActive = false;
            bool auctionEnterIsPayed = false;
            if (User.Identity.IsAuthenticated)
            {
                User user = UserService.GetUserByEmail(User.Identity.Name);

                userID = user.ID;
                if (auction.Auto.UserID == user.ID)
                    currentUserIsOwner = true;

                if (user.AuctionFavorites.FirstOrDefault(f => f.AuctionID == id) != null)
                    ViewBag.isInFavorite = true;

                //check if user enter auction the first time
                //if (user.AuctionAgreement == false && !currentUserIsOwner)
                //    return RedirectToAction("TermsOfUse", new { id = id });
            }
            else
            {
                string setting = $"{XCarsConfiguration.AllowUnauthenticatedUserToEnterAuction}";
                if (!string.IsNullOrWhiteSpace(setting) && setting == "false")
                    return HttpNotFound();
            }

            //try
            //{
                if (auction.StatusID == 2 && auction.Deadline > DateTime.Now)
                    isActive = true;

                if (!currentUserIsOwner && !isActive)
                    //return RedirectToAction("Index", "Home");
                    return HttpNotFound();

                //если пользователь еще не оплатил вход в этот аукцион, то снимаем деньги
                //если денег недостаточно, то перенаправляем на страницу пополнения баланса
                if (userID > 0 && !currentUserIsOwner)
                {
                    //User user = UserService.GetUserByEmail(User.Identity.Name);
                    //if (user.Payments.FirstOrDefault(p => p.Type == "AuctionEnter" && p.ObjectID == id) != null)
                    //    auctionEnterIsPayed = true;
                    //else
                    //{
                    //    decimal auctionEnterPrice = 0;
                    //    string setting = $"{XCarsConfiguration.AuctionEnterPrice}";
                    //    if (!string.IsNullOrWhiteSpace(setting))
                    //        Decimal.TryParse(setting, out auctionEnterPrice);

                    //    if (user.Balance >= auctionEnterPrice)
                    //    {
                    //        user.Balance -= auctionEnterPrice;
                    //        user.Payments.Add(new Payment()
                    //        {
                    //            Amount = auctionEnterPrice,
                    //            ObjectID = id,
                    //            Type = "AuctionEnter",
                    //            Description = Resource.AuctionEnterPayment
                    //        });
                    //        UserService.EditUser(user);

                    //        int eligiblePeriodInMinutes = 0;
                    //        string tmp = $"{XCarsConfiguration.XMinutesAuctionFinishEmailEligiblePeriod}";
                    //        int.TryParse(tmp, out eligiblePeriodInMinutes);

                    //        ScheduledEmail scheduledEmail = new ScheduledEmail()
                    //        {
                    //            DateScheduled = DateTime.Now,
                    //            DateDue = auction.Deadline,
                    //            //StatusID = 1,
                    //            To = user.Email,
                    //            Subject = "Subject1",
                    //            Body = "Text1",
                    //            ObjectTypeID = 2,
                    //            ObjectID = auction.ID
                    //        };
                    //        scheduledEmail.DateEligible = scheduledEmail.DateDue.AddMinutes(eligiblePeriodInMinutes);
                    //        ScheduledEmailService.Create(scheduledEmail);

                    //        int minutes = 0;
                    //        tmp = $"{XCarsConfiguration.XMinutesRemaingToAuctionDeadline}";
                    //        int.TryParse(tmp, out minutes);

                    //        eligiblePeriodInMinutes = 0;
                    //        tmp = $"{XCarsConfiguration.XMinutesRemainingAuctionFinishEmailEligiblePeriod}";
                    //        int.TryParse(tmp, out eligiblePeriodInMinutes);

                    //        ScheduledEmail scheduledEmail2 = new ScheduledEmail()
                    //        {
                    //            DateScheduled = DateTime.Now,
                    //            DateDue = auction.Deadline.AddMinutes(-1* minutes),
                    //            //StatusID = 1,
                    //            To = user.Email,
                    //            Subject = "Subject1",
                    //            Body = "Text1",
                    //            ObjectTypeID = 3,
                    //            ObjectID = auction.ID
                    //        };
                    //        scheduledEmail2.DateEligible = scheduledEmail2.DateDue.AddMinutes(eligiblePeriodInMinutes);
                    //        ScheduledEmailService.Create(scheduledEmail2);
                    //    }
                    //    else
                    //        return RedirectToAction("Pay", "Payment");
                    //}
                }

                ViewBag.userID = userID;
                ViewBag.currentUserIsOwner = currentUserIsOwner;
                ViewBag.isActive = isActive;
                ViewBag.auctionEnterIsPayed = auctionEnterIsPayed;
                ViewBag.UserNoPhotoUrl = $"{XCarsConfiguration.UserNoPhotoUrl}";
                ViewBag.ImageSourceType = $"{XCarsConfiguration.ImageSourceType}";

                AuctionDetailsVM auctionVM = auction;

                breadcrumbs.Add("/Auto/Index", auctionVM.Auto.Region);
                breadcrumbs.Add("/Auto", auctionVM.Auto.Make);
                breadcrumbs.Add("#", auctionVM.Auto.Make + " " + auctionVM.Auto.Model + " " + auctionVM.Auto.YearOfIssue);
                ViewBag.breadcrumbs = breadcrumbs;

                //create search model for getting related
                ExtendedSearchVM auctionSearchVM = new ExtendedSearchVM()
                {
                    //MakeAndModels = new List<MakeAndModelVM>() { new MakeAndModelVM() { MakeID = auto.MakeID, ModelID = auto.ModelID, ModelToBeExcluded = false } },
                    MakeID = new int[1] { auction.Auto.MakeID },
                    ModelID = new int[1] { auction.Auto.ModelID },
                    MakeAndModels = new List<MakeAndModelVM>() { new MakeAndModelVM() { MakeID = auction.Auto.MakeID, ModelID = auction.Auto.ModelID } },
                    YearOfIssueFrom = auction.Auto.YearOfIssue,
                    YearOfIssueTo = auction.Auto.YearOfIssue,
                    IDsToBeExcluded = new int[1] { auction.ID },
                    Type = "auction"
                };
                ViewBag.auctionSearchVM = auctionSearchVM;

                List<AuctionPhotoVM> orderedPhotos = auctionVM.AuctionPhotoes.OrderByDescending(p => p.IsMain).ToList();
                ViewBag.orderedPhotos = orderedPhotos;
                List<AutoPhotoVM> orderedAutoPhotos = auctionVM.Auto.AutoPhotoes.OrderByDescending(p => p.IsMain).ToList();
                ViewBag.orderedAutoPhotos = orderedAutoPhotos;

                AutoPhotoVM mainPhoto = orderedAutoPhotos[0];

                //Open Graph (for fb share purposes)
                Dictionary<string, string> openGraph = new Dictionary<string, string>();
                openGraph["url"] = "http://emcar.quadevs.com/Auction/Details/" + id;
                openGraph["type"] = "page";
                openGraph["title"] = auctionVM.Auto.Make + " " + auctionVM.Auto.Model + (!string.IsNullOrWhiteSpace(auctionVM.Auto.Modification) ? auctionVM.Auto.Modification : "") + " " + auctionVM.Auto.YearOfIssue;
                openGraph["description"] = auctionVM.Auto.Description + " " + auctionVM.Description;
                openGraph["image"] = XCarsConfiguration.BucketEndpoint + XCarsConfiguration.BucketName + "/" + XCarsConfiguration.AutoPhotosUploadUrl + (mainPhoto.ID != 0 ? (mainPhoto.ID + "_1024x768") : XCarsConfiguration.AutoNoPhotoName) + XCarsConfiguration.PhotoExtension;
                ViewBag.openGraph = openGraph;

                ViewBag.deadlineZFormat = string.Format("{0}Z", auctionVM.Deadline.ToString("s"));

                return View(auctionVM);
            //}
            //catch (Exception ex)
            //{
            //    return HttpNotFound();
            //}
        }

        [Authorize]
        public ActionResult TermsOfUse(int? id)
        {
            ViewBag.auctionID = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult TermsOfUse(ApplyTermOfUseOfAuctionVM model)
        {
            if (model.Agree == false)
            {
                ModelState.AddModelError("", Resource.PleaseCheck);
                ViewBag.auctionID = model.AuctionID;
                return View();
            }
            User user = UserService.GetUserByEmail(User.Identity.Name);
            user.AuctionAgreement = model.Agree;
            UserService.EditUser(user);

            if (model.AuctionID != null)
                return RedirectToAction("Details", "Auction", new { id = model.AuctionID });
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public void FinishAuction(int id)
        //{
        //    Auction auction = _auctionService.GetActiveByID(id);
        //    if (auction == null)
        //        return;

        //    auction.StateID = 2;
        //    _auctionService.Edit(auction);
        //}
    }
}