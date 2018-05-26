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
using XCars.Service;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    [Authorize]
    public class MyAuctionController : Controller
    {
        public IUserService UserService { get; set; }
        //public IAutoService AutoService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IAuctionService AuctionService { get; set; }
        //public IAuctionPhotoService AuctionPhotoService { get; set; }
        public IHangfireService HangfireService { get; set; }
        public IAuctionFavoriteService AuctionFavoriteService { get; set; }

        public IAutoStatusService _autoStatusService { get; set; } //should be removed and replaced by AuctionStatusService
        public IAuctionBidService AuctionBidService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public MyAuctionController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: Auction
        public ActionResult Index()
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            UserShortVM userVM = user;
            ViewBag.userVM = userVM;

            ViewBag.statuses = _autoStatusService.GetAllAsSelectList(0);
            ViewBag.selectedStatusID = 0;

            ViewBag.userID = user.ID;

            //breadcrumbs.Add("/MyCabinet/Index", Resource.MyCabinet);
            //breadcrumbs.Add("#", Resource.MyAuctions);
            breadcrumbs.Add("#", Resource.MyCabinet);
            ViewBag.breadcrumbs = breadcrumbs;
            ViewBag.activeTab = "MyAuctions";

            return View(new ExtendedSearchVM());
        }

        public ActionResult Create(int id) //auto ID
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            Auto auto = user?.Autoes.FirstOrDefault(a => a.ID == id && a.StatusID == 2);
            if (auto == null)
                return HttpNotFound();

            Auction auction = new Auction()
            {
                AutoID = auto.ID,
                DateCreated = DateTime.Now,
                StartPrice = 0,
                CurrentPrice = 0,
                PriceUSDSearch = 0,
                PriceUAHSearch = 0,
                CurrencyID = 1,
                StatusID = 1, //draft
                Deadline = DateTime.Now
            };
            AuctionService.Create(auction);
            //string jobID = HangfireService.CreateJobForAuctionDeletion(auction);
            //auction.DeletionJobID = jobID;
            //AuctionService.Edit(auction);

            ViewBag.currencies = CurrencyService.GetAllAsSelectList();
            ViewBag.recommendedPrice = AuctionService.GetRecommendedPrice(auto.PriceUSD, auto.PriceUAH);

            AuctionCreateVM auctionCreateVM = auction;

            AutoDetailsVM autoVM = auto;
            List<AutoPhotoVM> orderedPhotos = autoVM.AutoPhotoes.OrderByDescending(p => p.IsMain).ToList();
            AutoPhotoVM mainPhoto = orderedPhotos[0];
            ViewBag.mainPhoto = mainPhoto;

            ViewBag.autoVM = autoVM;

            breadcrumbs.Add("#", Resource.AuctionCreate);
            ViewBag.breadcrumbs = breadcrumbs;

            int limit = 2000;
            int.TryParse(XCarsConfiguration.AutoDescriptionMaxLength, out limit);
            ViewBag.autoDescriptionMaxLength = limit;

            return View(auctionCreateVM);
        }

        [HttpPost]
        public ActionResult Create(AuctionCreateVM modelVM)
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            Auto auto = user?.Autoes.FirstOrDefault(a => a.ID == modelVM.AutoID);
            if (auto == null)
                return HttpNotFound();

            Auction auction = AuctionService.GetUnactive(modelVM.ID);
            if (auction == null || auction.AutoID != auto.ID)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //auction.AutoID = modelVM.AutoID;
                    auction.StartPrice = modelVM.StartPrice;
                    auction.CurrentPrice = modelVM.StartPrice;
                    auction.CurrencyID = modelVM.CurrencyID;
                    auction.Description = modelVM.Description;
                    auction.DateCreated = DateTime.Now;
                    auction.Deadline = DateTime.Now.AddHours(modelVM.Hours + modelVM.Days*24);
                    //auction.Deadline = DateTime.Now.AddMinutes(modelVM.Hours);
                    //auction.Deadline = DateTime.Now.AddMinutes(2);
                    auction.StatusID = 2;

                    List<AuctionBid> bids = auction.AuctionBids.ToList();
                    foreach (var item in bids)
                        AuctionBidService.Delete(item);

                    //AuctionService.Edit(auction);
                    HangfireService.CancelJob(auction.DeletionJobID);
                    auction.CompletionJobID = HangfireService.CreateJobForAuction(auction);
                    AuctionService.Edit(auction);

                    return RedirectToAction("Details", "Auction", new { id = auction.ID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", Resource.SaveError + ": " + ex.Message);
                }
            }
            else
                ModelState.AddModelError("", Resource.InvalidData);

            //ViewBag.autoID = auto.ID;
            //ViewBag.auctionID = auction.ID;
            ViewBag.currencies = CurrencyService.GetAllAsSelectList();
            ViewBag.recommendedPrice = AuctionService.GetRecommendedPrice(auto.PriceUSD, auto.PriceUAH);

            breadcrumbs.Add("#", Resource.AuctionCreate);
            ViewBag.breadcrumbs = breadcrumbs;

            int limit = 2000;
            int.TryParse(XCarsConfiguration.AutoDescriptionMaxLength, out limit);
            ViewBag.autoDescriptionMaxLength = limit;

            return View(modelVM);
        }

        public ActionResult Publish(int id) //auction ID
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            Auction auction = AuctionService.GetByID(id);
            if (auction == null || auction.Auto.UserID != user.ID)
                return HttpNotFound();

            ViewBag.currencies = CurrencyService.GetAllAsSelectList();
            ViewBag.recommendedPrice = AuctionService.GetRecommendedPrice(auction.Auto.PriceUSD, auction.Auto.PriceUAH);

            AuctionCreateVM auctionCreateVM = auction;

            AutoDetailsVM autoVM = auction.Auto;
            List<AutoPhotoVM> orderedPhotos = autoVM.AutoPhotoes.OrderByDescending(p => p.IsMain).ToList();
            AutoPhotoVM mainPhoto = orderedPhotos[0];
            ViewBag.mainPhoto = mainPhoto;

            ViewBag.autoVM = autoVM;

            breadcrumbs.Add("#", Resource.AuctionCreate);
            ViewBag.breadcrumbs = breadcrumbs;

            int limit = 2000;
            int.TryParse(XCarsConfiguration.AutoDescriptionMaxLength, out limit);
            ViewBag.autoDescriptionMaxLength = limit;

            return View("Create", auctionCreateVM);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult FinishManually(int id)
        {
            try
            {
                Auction auction = AuctionService.GetByID(id);
                bool finishManually = true;
                AuctionService.Finish(auction, finishManually);
                HangfireService.CancelJob(auction.CompletionJobID);

                return Json(new { result = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult MoveToArchives(int id)
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);
            Auction auction = AuctionService.GetByID(id);
            if (auction == null || auction.Auto.UserID != user.ID)
                return HttpNotFound();

            try
            {
                bool moveManually = true;
                AuctionService.Finish(auction, moveManually);
                HangfireService.CancelJob(auction.CompletionJobID);
                HangfireService.CancelJob(auction.DeletionJobID);
            }
            catch
            {
                return HttpNotFound();
            }

            Thread.Sleep(1000);
            return RedirectToAction("Index", "MyAuto");
        }

        [HttpPost]
        public ActionResult AddToFavorites(int id)
        {
            var ctrl = new Apis.MyAuctionController(AuctionService, UserService, AuctionFavoriteService);
            var response = ctrl.AddToFavorites(id) as OkNegotiatedContentResult<int>;

            return Json(new { result = response.Content });
        }
    }
}