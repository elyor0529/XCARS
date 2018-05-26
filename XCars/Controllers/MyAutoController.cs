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
using System.Security.Cryptography;
using System.Text;

namespace XCars.Controllers
{
    [Authorize]
    public class MyAutoController : Controller
    {
        public IUserService _userService { get; set; }
        public IAutoService _autoService { get; set; }
        public IAutoSecurityService _autoSecurityService { get; set; }
        public IAutoComfortService _autoComfortService { get; set; }
        public IAutoMultimediaService _autoMultimediaService { get; set; }
        public IAutoStateService _autoStateService { get; set; }
        public IAutoMiscService _autoMiscService { get; set; }
        //public IFileManager _fileManager { get; set; }
        //public IAutoPhotoService _autoPhotoService { get; set; }
        public IAutoStatusService _autoStatusService { get; set; }
        public IBillingService _billingService { get; set; }
        public IAutoFavoriteService _autoFavoriteService { get; set; }
        public IHangfireService HangfireService { get; set; }
        public ISearchAutoService SearchAutoService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IOrderService OrderService { get; set; }
        public IPurchaseTypeService PurchaseTypeService { get; set; }
        public IHash Hash { get; set; }
        public IAuctionStatusService AuctionStatusService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public MyAutoController()
        {
            breadcrumbs.Add("/", Resource.Main);
            //breadcrumbs.Add("/MyCabinet/Index", Resource.MyCabinet);
        }

        public ActionResult Index(int statusID = 0, string error = null)
        {
            if (statusID == 0)
                statusID = 2;
            User user = _userService.GetUserByEmail(User.Identity.Name);
            UserShortVM userVM = user;
            ViewBag.userVM = userVM;

            ViewBag.statuses = _autoStatusService.GetAllAsSelectList(statusID);
            ViewBag.selectedStatusID = statusID;

            if (error != null)
                ViewBag.error = error;

            ViewBag.userID = user.ID;

            //breadcrumbs.Add("#", Resource.MyAdvertisements);
            breadcrumbs.Add("#", Resource.MyCabinet);
            ViewBag.breadcrumbs = breadcrumbs;
            ViewBag.activeTab = "MyAutos";

            ExtendedSearchVM searchVM = new ExtendedSearchVM()
            {
                UserID = user.ID
            };
            if (statusID > 0)
                searchVM.StatusID = statusID;

            ViewBag.auctionSearchVM = new ExtendedSearchVM()
            {
                UserID = user.ID,
                StatusID = 1,
                Type = "auction"
            };

            ViewBag.auctionStatuses = AuctionStatusService.GetAllAsSelectList();

            return View(searchVM);
        }

        public ActionResult Checkout(int id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();

            int topMin = 0;
            int.TryParse(XCarsConfiguration.AutoPublishTopMin, out topMin);
            int topMax = 750;
            int.TryParse(XCarsConfiguration.AutoPublishTopMax, out topMax);
            int daysMin = 1;
            int.TryParse(XCarsConfiguration.AutoPublishDaysMin, out daysMin);
            int daysMax = 90;
            int.TryParse(XCarsConfiguration.AutoPublishDaysMax, out daysMax);

            ViewBag.topMin = topMin;
            ViewBag.topMax = topMax;
            ViewBag.daysMin = daysMin;
            ViewBag.daysMax = daysMax;

            int oncePayedCost = 50;
            int.TryParse(XCarsConfiguration.AutoPublishOncePayedCost, out oncePayedCost);
            int topDefault = 40;
            int.TryParse(XCarsConfiguration.AutoPublishTopDefault, out topDefault);
            if (topDefault < topMin || topDefault > topMax)
                topDefault = (topMin + topMax) / 2;
            int daysDefault = 20;
            int.TryParse(XCarsConfiguration.AutoPublishDaysDefault, out daysDefault);
            if (daysDefault < daysMin || daysDefault > daysMax)
                daysDefault = (daysMin + daysMax) / 2;

            ViewBag.positions = SearchAutoService.GetPotentialPositions(auto.ID, auto.RegionID, auto.MakeID, auto.ModelID, topDefault);

            ViewBag.oncePayedCost = oncePayedCost;
            ViewBag.topDefault = topDefault;
            ViewBag.daysDefault = daysDefault;

            breadcrumbs.Add("/MyAuto/Index", Resource.MyAdvertisements);
            breadcrumbs.Add("/MyAuto/Details/" + id, auto.AutoMake.Name + " " + auto.AutoModel.Name);
            breadcrumbs.Add("#", Resource.Checkout);
            ViewBag.breadcrumbs = breadcrumbs;

            return View(auto);
        }

        [HttpPost]
        public ActionResult Publish(int id, int top, int days, bool balanceUsed = false)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();

            decimal oncePayedCost = 50;
            decimal.TryParse(XCarsConfiguration.AutoPublishOncePayedCost, out oncePayedCost);
            decimal price = _billingService.GeneratePriceForAutoPublishing(oncePayedCost, top, days);
            decimal totalToBePayed = price;

            if (balanceUsed && user.Balance > 0)
                totalToBePayed = price - user.Balance;
            if (totalToBePayed < 0)
                totalToBePayed = 0;

            //create order
            int merchantID = 0;
            int.TryParse(XCarsConfiguration.LMI_MERCHANT_ID, out merchantID);

            int lmi_mode = 0;
            int.TryParse(XCarsConfiguration.LMI_MODE, out lmi_mode);

            try
            {
                PurchaseType purchaseType = PurchaseTypeService.GetByID(1);
                string paymentDescription = purchaseType?.Name ?? "autoPublish";

                Order order = new Order()
                {
                    LMI_MERCHANT_ID = merchantID,
                    LMI_PAYMENT_AMOUNT = totalToBePayed,
                    LMI_MODE = lmi_mode,
                    UserID = user.ID,
                    DateCreated = DateTime.Now,
                    PurchaseTypeID = 1,
                    ObjectID = auto.ID,
                    IsOpen = true
                };

                OrderService.Create(order);

                order.LMI_HASH = Hash.GetHash(order.LMI_MERCHANT_ID + order.LMI_PAYMENT_NO + order.LMI_PAYMENT_AMOUNT.ToString().Replace(',', '.') + XCarsConfiguration.LMI_secretKey);

                if (price == totalToBePayed)
                    order.UsedFromBalance = 0;
                else if (price > totalToBePayed)
                    order.UsedFromBalance = price - totalToBePayed;

                order.OrderDetails.Add(new OrderDetail() { Name = "top", Value = top.ToString() });
                order.OrderDetails.Add(new OrderDetail() { Name = "days", Value = days.ToString() });

                OrderService.Edit(order);

                string successUrl = XCarsConfiguration.AutoPublishSuccessUrl;
                string failUrl = XCarsConfiguration.AutoPublishPaymentFailUrl;

                if (totalToBePayed > 0)
                {
                    Uri url = new Uri(Request.Url.AbsoluteUri);
                    string hostAndPort = url.GetLeftPart(UriPartial.Authority);

                    return Json(new
                    {
                        LMI_MERCHANT_ID = merchantID,
                        LMI_PAYMENT_NO = order.LMI_PAYMENT_NO,
                        LMI_PAYMENT_DESC = paymentDescription,
                        LMI_SUCCESS_URL = hostAndPort + successUrl,
                        LMI_FAIL_URL = hostAndPort + failUrl,
                        LMI_HASH = order.LMI_HASH
                    });
                }
                else
                {
                    order.IsOpen = false;
                    OrderService.Edit(order);

                    user.Balance -= price;

                    //DateTime dateExpires = DateTime.Now.AddDays(days);
                    DateTime dateExpires = DateTime.Now.AddMinutes(2);
                    _autoService.Publish(auto, dateExpires);
                    _userService.EditUser(user);
                    auto.CompletionJobID = HangfireService.CreateJobForAuto(auto);
                    auto.Top = top;
                    _autoService.Edit(auto);

                    return Json(new { ok = 1, successUrl = successUrl });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult MoveToArchives(int id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();

            try
            {
                bool moveManually = true;
                _autoService.MoveToArchives(auto, moveManually);
                HangfireService.CancelJob(auto.CompletionJobID);
            }
            catch
            {
                return HttpNotFound();
            }

            Thread.Sleep(1000);
            return RedirectToAction("Index", new { statusID = 3 });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();

            try
            {
                _autoService.Delete(auto);
            }
            catch
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddMainInfo(int? id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            AutoMainInfoVM autoMainInfoVM;

            //breadcrumbs.Add("/MyAuto/Index", Resource.MyAdvertisements);

            ViewBag.activeTab = "AddMainInfo";

            if (id == null)
            {
                breadcrumbs.Add("#", Resource.AddNewAd);
                ViewBag.breadcrumbs = breadcrumbs;

                return View(new AutoMainInfoVM());
            }

            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();
            else
                autoMainInfoVM = auto;

            breadcrumbs.Add("#", Resource.EditAdvertisement);
            ViewBag.breadcrumbs = breadcrumbs;
            ViewBag.autoID = id;

            return View(autoMainInfoVM);
        }

        [HttpPost]
        public ActionResult AddMainInfo(AutoMainInfoVM autoMainInfoVM)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                if (autoMainInfoVM.PriceUSD != null || autoMainInfoVM.PriceUAH != null)
                {
                    try
                    {
                        Auto model = user.Autoes.FirstOrDefault(a => a.ID == autoMainInfoVM.AutoID);
                        if (model == null)
                        {
                            model = new Auto();
                            model.UserID = user.ID;
                            model.DateCreated = DateTime.Now;
                            model.DateUpdated = model.DateCreated;
                            model.IsColorMetallic = false;
                            model.StatusID = 1;
                            model.Top = 0;
                        }
                        else
                            model.DateUpdated = DateTime.Now;

                        model.RegionID = autoMainInfoVM.CityID;
                        model.TransportTypeID = autoMainInfoVM.TransportTypeID;
                        model.BodyTypeID = autoMainInfoVM.BodyTypeID;
                        model.MakeID = autoMainInfoVM.MakeID;
                        model.ModelID = autoMainInfoVM.ModelID;
                        model.NumberOfDoors = autoMainInfoVM.NumberOfDoors;
                        model.NumberOfSeats = autoMainInfoVM.NumberOfSeats;
                        model.YearOfIssue = autoMainInfoVM.YearOfIssue;
                        model.Modification = autoMainInfoVM.Modification;
                        model.TSRegistrationID = autoMainInfoVM.TSRegistrationID;
                        //model.VINCode = autoMainInfoVM.VINCode;
                        model.TransmissionTypeID = autoMainInfoVM.TransmissionTypeID;
                        model.EngineCapacity = autoMainInfoVM.EngineCapacity;
                        model.Probeg = autoMainInfoVM.Probeg;
                        model.PriceUSD = autoMainInfoVM.PriceUSD;
                        model.PriceUAH = autoMainInfoVM.PriceUAH;
                        model.CurrencyID = autoMainInfoVM.PriceUSD != null ? 1 : 2;

                        //double currencyRate = CurrencyService.GetCurrencyRate();
                        //model.PriceUSDSearch = (model.PriceUSD != null) ? (int)model.PriceUSD : (int)(model.PriceUAH / currencyRate);
                        //model.PriceUAHSearch = (model.PriceUAH != null) ? (int)model.PriceUAH : (int)(model.PriceUSD * currencyRate);

                        //model.IsRastamojed = autoMainInfoVM.IsRastamojed;
                        model.IsTorgAvailable = autoMainInfoVM.IsTorgAvailable;
                        model.IsExchangeAvailable = autoMainInfoVM.IsExchangeAvailable;
                        model.ColorID = autoMainInfoVM.ColorID;
                        model.IsColorMetallic = autoMainInfoVM.IsColorMetallic;
                        model.FuelTypeID = autoMainInfoVM.FuelTypeID;
                        model.DriveTypeID = autoMainInfoVM.DriveTypeID;

                        if (model.ID == 0)
                            _autoService.Create(model);
                        else
                            _autoService.Edit(model);

                        return RedirectToAction("AddExtraInfo", new { id = model.ID });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", Resource.SaveError + ": " + ex.Message);
                    }
                }
                else
                    ModelState.AddModelError("", Resource.ProvidePrice);
            }
            else
                ModelState.AddModelError("", Resource.InvalidData);

            ViewBag.activeTab = "AddMainInfo";

            //breadcrumbs.Add("/MyAuto/Index", Resource.MyAdvertisements);
            if (autoMainInfoVM.AutoID == 0)
                breadcrumbs.Add("#", Resource.AddNewAd);
            else
            {
                ViewBag.autoID = autoMainInfoVM.AutoID;
                breadcrumbs.Add("#", Resource.EditAdvertisement);
            }

            ViewBag.breadcrumbs = breadcrumbs;

            return View(autoMainInfoVM);
        }

        public ActionResult AddExtraInfo(int id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            AutoExtraInfoVM autoExtraInfoVM;

            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();
            else
                autoExtraInfoVM = auto;

            //breadcrumbs.Add("/MyAuto/Index", Resource.MyAdvertisements);
            breadcrumbs.Add("#", Resource.EditAdvertisement);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.activeTab = "AddExtraInfo";
            ViewBag.autoID = id;

            int limit = 2000;
            int.TryParse(XCarsConfiguration.AutoDescriptionMaxLength, out limit);
            ViewBag.autoDescriptionMaxLength = limit;

            return View(autoExtraInfoVM);
        }

        [HttpPost]
        public ActionResult AddExtraInfo(AutoExtraInfoVM autoExtraInfoVM, string saveAsDraft)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto model = user.Autoes.FirstOrDefault(a => a.ID == autoExtraInfoVM.AutoID);
            if (model == null)
                return HttpNotFound();

            int limit = 2000;
            int.TryParse(XCarsConfiguration.AutoDescriptionMaxLength, out limit);
            ViewBag.autoDescriptionMaxLength = limit;

            if (autoExtraInfoVM.Description != null && autoExtraInfoVM.Description.Length > limit)
                autoExtraInfoVM.Description = autoExtraInfoVM.Description.Substring(0, limit);

            if (ModelState.IsValid)
            {
                try
                {
                    model.DateUpdated = DateTime.Now;
                    
                    
                    
                    model.FuelConsumptionCity = autoExtraInfoVM.FuelConsumptionCity;
                    model.FuelConsumptionHighway = autoExtraInfoVM.FuelConsumptionHighway;
                    model.FuelConsumptionMixed = autoExtraInfoVM.FuelConsumptionMixed;
                    
                    model.Power = autoExtraInfoVM.Power;
                    model.Description = autoExtraInfoVM.Description;
                    model.PlaceLat = autoExtraInfoVM.PlaceLat;
                    model.PlaceLng = autoExtraInfoVM.PlaceLng;

                    model.AutoSecurities.Clear();
                    if (autoExtraInfoVM.AutoSecurities != null)
                        for (int i = 0; i < autoExtraInfoVM.AutoSecurities.Length; i++)
                            model.AutoSecurities.Add(_autoSecurityService.GetByID(autoExtraInfoVM.AutoSecurities[i]));
                    else
                        autoExtraInfoVM.AutoSecurities = new int[0];

                    model.AutoComforts.Clear();
                    if (autoExtraInfoVM.AutoComforts != null)
                        for (int i = 0; i < autoExtraInfoVM.AutoComforts.Length; i++)
                            model.AutoComforts.Add(_autoComfortService.GetByID(autoExtraInfoVM.AutoComforts[i]));
                    else
                        autoExtraInfoVM.AutoComforts = new int[0];

                    model.AutoMultimedias.Clear();
                    if (autoExtraInfoVM.AutoMultimedias != null)
                        for (int i = 0; i < autoExtraInfoVM.AutoMultimedias.Length; i++)
                         model.AutoMultimedias.Add(_autoMultimediaService.GetByID(autoExtraInfoVM.AutoMultimedias[i]));
                    else
                        autoExtraInfoVM.AutoMultimedias = new int[0];

                    model.AutoStates.Clear();
                    if (autoExtraInfoVM.AutoStates != null)
                        for (int i = 0; i < autoExtraInfoVM.AutoStates.Length; i++)
                            model.AutoStates.Add(_autoStateService.GetByID(autoExtraInfoVM.AutoStates[i]));
                    else
                        autoExtraInfoVM.AutoStates = new int[0];

                    model.AutoMiscs.Clear();
                    if (autoExtraInfoVM.AutoMiscs != null)
                        for (int i = 0; i < autoExtraInfoVM.AutoMiscs.Length; i++)
                            model.AutoMiscs.Add(_autoMiscService.GetByID(autoExtraInfoVM.AutoMiscs[i]));
                    else
                        autoExtraInfoVM.AutoMiscs = new int[0];

                    _autoService.Edit(model);

                    if (saveAsDraft != null)
                        return RedirectToAction("Index", new { statusID = 1 });
                    else
                        return RedirectToAction("Preview", new { id = model.ID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", Resource.SaveError + ": " + ex.Message);
                }
            }
            else
                ModelState.AddModelError("", Resource.InvalidData);

            if (autoExtraInfoVM.AutoPhotoes == null)
                autoExtraInfoVM.AutoPhotoes = new List<AutoPhotoVM>();
            else
                autoExtraInfoVM.AutoPhotoes.Clear();
            foreach (var item in model.AutoPhotoes)
                autoExtraInfoVM.AutoPhotoes.Add(item);

            if (autoExtraInfoVM.AutoSecurities == null)
                autoExtraInfoVM.AutoSecurities = new int[0];
            if (autoExtraInfoVM.AutoComforts == null)
                autoExtraInfoVM.AutoComforts = new int[0];
            if (autoExtraInfoVM.AutoMultimedias == null)
                autoExtraInfoVM.AutoMultimedias = new int[0];
            if (autoExtraInfoVM.AutoStates == null)
                autoExtraInfoVM.AutoStates = new int[0];
            if (autoExtraInfoVM.AutoMiscs == null)
                autoExtraInfoVM.AutoMiscs = new int[0];

            //breadcrumbs.Add("/MyAuto/Index", Resource.MyAdvertisements);
            breadcrumbs.Add("#", Resource.EditAdvertisement);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.activeTab = "AddExtraInfo";
            ViewBag.autoID = autoExtraInfoVM.AutoID;

            return View(autoExtraInfoVM);
        }

        public ActionResult Preview(int id)
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);
            Auto auto = user.Autoes.FirstOrDefault(a => a.ID == id);
            if (auto == null)
                return HttpNotFound();

            double dollarRate = CurrencyService.GetCurrencyRate();

            AutoShortInfoVM autoShortInfoVM = auto;

            breadcrumbs.Add("#", Resource.AddAuto);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.activeTab = "Preview";
            ViewBag.autoID = id;

            return View(autoShortInfoVM);
        }

        [HttpPost]
        public ActionResult AddToFavorites(int id)
        {
            var ctrl = new Apis.MyAutoController(_autoService, _userService, _autoFavoriteService);
            var response = ctrl.AddToFavorites(id) as OkNegotiatedContentResult<int>;

            return Json(new { result = response.Content });
        }

        [ChildActionOnly]
        public int ShowFavoritesCount()
        {
            User user = _userService.GetUserByEmail(User.Identity.Name);

            return _autoFavoriteService.GetCountOfUserFavorites(user);
        }

        //[HttpPost]

    }
}