using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    public class AutoController : Controller
    {
        public IUserService UserService { get; set; }
        public IAutoService AutoService { get; set; }
        public IFileManager FileManager { get; set; }
        public IAutoPhotoService AutoPhotoService { get; set; }
        public IContactsRequestService ContactsRequestService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IAutoExchangeService AutoExchangeService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public AutoController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: Auto
        public ActionResult Index()
        {
            breadcrumbs.Add("#", Resource.Advertisements);
            ViewBag.breadcrumbs = breadcrumbs;

            ExtendedSearchVM modelVM = new ExtendedSearchVM()
            {
                StatusID = 2
            };

            return View("~/Views/SearchAuto/SearchResult.cshtml", modelVM);
        }

        public ActionResult Details(int id)
        {
            //Auto auto = AutoService.GetPublishedByID(id);
            Auto auto = AutoService.GetByID(id);
            if (auto == null)
                return HttpNotFound();

            auto.Views++;
            AutoService.Edit(auto);

            User currentUser = null;
            if (User.Identity.IsAuthenticated)
                currentUser = UserService.GetUserByEmail(User.Identity.Name);

            //double dollarRate = CurrencyService.GetCurrencyRate();

            AutoDetailsVM autoDetailsVM = auto;

            for (int i = 0; i < autoDetailsVM.AutoExchangesIncome.Count; i++)
            {
                autoDetailsVM.AutoExchangesIncome[i].DeleteButtonIsAvailable = (currentUser != null && (currentUser.ID == auto.UserID || currentUser.ID == autoDetailsVM.AutoExchangesIncome[i].Auto.Owner.UserID));
            }
                

            List<AutoShortInfoVM> autosAvailableForExchangeOffer = null;
            if (currentUser != null)
            {
                List<Auto> tmp = currentUser.Autoes.Where(a => /*a.StatusID == 2
                                                            &&*/ a.AutoExchangesOutcome.FirstOrDefault(ex => ex.TargetAutoID == id) == null).ToList();
                autosAvailableForExchangeOffer = new List<AutoShortInfoVM>();
                foreach (var item in tmp)
                    autosAvailableForExchangeOffer.Add(item);

                ViewBag.currencies = CurrencyService.GetAllAsSelectList(1);
                //ViewBag.showAddtoFavoriteButton = true;

                if (currentUser.AutoFavorites.FirstOrDefault(f => f.AutoID == id) != null)
                    ViewBag.isInFavorite = true;

                if (currentUser.ID == autoDetailsVM.Owner.UserID)
                    ViewBag.isAbleToAnswerToOffers = true;
            }

            ViewBag.autosAvailableForExchangeOffer = autosAvailableForExchangeOffer;

            //breadcrumbs.Add("/Auto/Index", Resource.Advertisements);
            breadcrumbs.Add("/Auto/Index", autoDetailsVM.Region);
            breadcrumbs.Add("/Auto", autoDetailsVM.Make);
            breadcrumbs.Add("#", autoDetailsVM.Make + " " + autoDetailsVM.Model + " " + autoDetailsVM.YearOfIssue);
            ViewBag.breadcrumbs = breadcrumbs;

            //create search model for getting related
            ExtendedSearchVM searchVM = new ExtendedSearchVM()
            {
                //MakeAndModels = new List<MakeAndModelVM>() { new MakeAndModelVM() { MakeID = auto.MakeID, ModelID = auto.ModelID, ModelToBeExcluded = false } },
                MakeID = new int[1] { auto.MakeID },
                ModelID = new int[1] { auto.ModelID },
                MakeAndModels = new List<MakeAndModelVM>() { new MakeAndModelVM() { MakeID = auto.MakeID, ModelID = auto.ModelID } },
                YearOfIssueFrom = auto.YearOfIssue,
                YearOfIssueTo = auto.YearOfIssue,
                IDsToBeExcluded = new int[1] { auto.ID }
            };
            ViewBag.searchVM = searchVM;

            List<AutoPhotoVM> orderedPhotos = autoDetailsVM.AutoPhotoes.OrderByDescending(p => p.IsMain).ToList();
            AutoPhotoVM mainPhoto = orderedPhotos[0];
            ViewBag.orderedPhotos = orderedPhotos;

            //Open Graph (for fb share purposes)
            Dictionary<string, string> openGraph = new Dictionary<string, string>();
            openGraph["url"] = "http://emcar.quadevs.com/Auto/Details/" + id;
            openGraph["type"] = "page";
            openGraph["title"] = autoDetailsVM.Make + " " + autoDetailsVM.Model + (!string.IsNullOrWhiteSpace(autoDetailsVM.Modification) ? autoDetailsVM.Modification : "") + " " + autoDetailsVM.YearOfIssue;
            openGraph["description"] = autoDetailsVM.Description;
            openGraph["image"] = XCarsConfiguration.BucketEndpoint + XCarsConfiguration.BucketName + "/" + XCarsConfiguration.AutoPhotosUploadUrl + (mainPhoto.ID != 0 ? (mainPhoto.ID + "_575x359") : XCarsConfiguration.AutoNoPhotoName) + XCarsConfiguration.PhotoExtension;
            ViewBag.openGraph = openGraph;

            return View(autoDetailsVM);
        }

        [HttpPost]
        public ActionResult ContactsRequestIncrement(int autoID)
        {
            string result = "error#" + Resource.UnknownError;
            Auto auto = AutoService.GetPublishedByID(autoID);
            if (auto == null)
                result = "error#" + Resource.NotFound;
            else
            {
                try
                {
                    ContactsRequest request = new ContactsRequest()
                    {
                        AutoID = autoID,
                        UserID = auto.UserID
                    };
                    ContactsRequestService.Create(request);

                    result = "success#1";
                }
                catch (Exception ex)
                {
                    result = "error#" + ex.Message;
                }
            }

            return Json(result);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddExchangeOffer(AddExchangeOfferVM model)
        {
            Auto targetAuto = AutoService.GetPublishedByID(model.TargetAutoID);
            if (targetAuto == null)
                return HttpNotFound();

            User currentUser = UserService.GetUserByEmail(User.Identity.Name);
            AutoExchangeDetailsVM exchangeDetailsVM = null;

            try
            {
                AutoExchange newExchange = new AutoExchange()
                {
                    OfferedAutoID = model.OfferedAutoID,
                    TargetAutoID = model.TargetAutoID,
                    DateCreated = DateTime.Now,
                    CurrencyID = model.CurrencyID,
                    DiffPrice = model.DiffPrice,
                    DiffPriceDirection = model.DiffPriceDirection
                };
                AutoExchangeService.Create(newExchange);

                Currency currency = CurrencyService.GetByID(newExchange.CurrencyID);

                Auto offeredAuto = AutoService.GetByID(model.OfferedAutoID);
                exchangeDetailsVM = targetAuto.AutoExchangesIncome.FirstOrDefault(ex => ex.ID == newExchange.ID);
                if (exchangeDetailsVM != null)
                {
                    exchangeDetailsVM.Currency = currency.Symbol;
                    exchangeDetailsVM.DeleteButtonIsAvailable = (currentUser != null && (currentUser.ID == offeredAuto.UserID || currentUser.ID == offeredAuto.UserID));
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMsg = "Error: " + ex.Message;
            }

            return PartialView("_ExchangeOffer", exchangeDetailsVM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteOffer(int id)
        {
            User currentUser = UserService.GetUserByEmail(User.Identity.Name);
            string result = "error#" + Resource.NotFound;
            try
            {
                AutoExchange exchange = AutoExchangeService.GetByID(id);
                if (exchange == null)
                    throw new Exception(Resource.NotFound);

                if (exchange.AutoOffered.UserID == currentUser.ID || exchange.AutoTarget.UserID == currentUser.ID)
                {
                    AutoExchangeService.Delete(exchange);
                    result = "success#1";
                }
                else
                    throw new Exception(Resource.ThisOfferIsNotYours);
            }
            catch (Exception ex)
            {
                result = "error#" + ex.Message;
            }

            return Json(result);
        }

        //public ActionResult Related(int id)
        //{
        //    Auto auto = AutoService.GetPublishedByID(id);
        //    if (auto == null)
        //        return HttpNotFound();

        //    breadcrumbs.Add("/Auto/Index", Resource.Advertisements);
        //    breadcrumbs.Add("/Auto/Details/" + auto.ID, auto.AutoMake.Name + " " + auto.AutoModel.Name + ", " + auto.YearOfIssue);
        //    breadcrumbs.Add("#", Resource.Related);
        //    ViewBag.breadcrumbs = breadcrumbs;

        //    return View(id);
        //}

        //public ActionResult GetRelated(int draw, int start, int length, int id)
        //{
        //    IEnumerable<Auto> autos = AutoService.GetRelated(id);
        //    int recordsFiltered = autos.Count();

        //    List<List<string>> data = new List<List<string>>();
        //    string error = "";

        //    try
        //    {
        //        double dollarRate = CurrencyService.GetCurrencyRate();
        //        List<AutoShortInfoVM> autosVM = new List<AutoShortInfoVM>();
        //        List<Auto> autosList = autos.OrderByDescending(a => a.DatePublished).Skip(start).Take(length).ToList();
        //        foreach (var item in autosList)
        //            autosVM.Add(item);

        //        string usdSymbol = CurrencyService.GetByID(1).Symbol;
        //        string uahSymbol = CurrencyService.GetByID(2).Symbol;

        //        for (int i = 0; i < autosVM.Count; i++)
        //        {
        //            autosVM[i].PriceUSD = (autosList[i].PriceUSD != null) ? autosList[i].PriceUSD : (autosList[i].PriceUAH != null ? (int)(autosList[i].PriceUAH / dollarRate) : 0);
        //            autosVM[i].PriceUAH = (autosList[i].PriceUAH != null) ? autosList[i].PriceUAH : (autosList[i].PriceUSD != null ? (int)(autosList[i].PriceUSD * dollarRate) : 0);

        //            //autosVM[i].PriceUSDStr = usdSymbol + " " + autosList[i].PriceUSD.ToString();
        //            //autosVM[i].PriceUAHStr = autosList[i].PriceUAH.ToString() + " " + uahSymbol;

        //            string imageBox = "<img class='img-thumbnail img-responsive async' finalSrc='" + autosVM[i].Photo.Src + "' src='" + XCars.Common.XCarsConfiguration.ClearGif + "'/>";

        //            string info = "<h4><a href='/Auto/Details/" + autosVM[i].ID + "'>" + autosVM[i].Make + " " + autosVM[i].Model + ", " + autosVM[i].YearOfIssue + "</a></h4>";
        //            data.Add(new List<string>() { imageBox, info });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.Message;
        //    }

        //    return Json(new { draw = draw, recordsFiltered = recordsFiltered, data = data, error = error }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetAllActive()
        {
            var ctrl = new Apis.AutoController(AutoService);
            var response = ctrl.GetAllActive() as OkNegotiatedContentResult<List<object>>;

            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
    }
}