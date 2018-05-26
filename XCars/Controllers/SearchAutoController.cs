using System;
using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service;
using XCars.Service.Interfaces;
using XCars.Services;
using XCars.ViewModels;

namespace XCars.Controllers
{
    public class SearchAutoController : Controller
    {
        public IUserService UserService { get; set; }
        public ISearchAutoService SearchAutoService { get; set; }
        //public IFileManager FileManager { get; set; }
        public ICityService CityService { get; set; }
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IYearService YearService { get; set; }
        public IAutoStatisticsService AutoStatisticsService { get; set; }

        public ISearchAuctionService SearchAuctionService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public SearchAutoController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: SearchAuto
        public ActionResult Index()
        {
            breadcrumbs.Add("#", Resource.Search);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.regions = CityService.GetAllAsSelectList();
            ViewBag.autoTransportTypes = AutoTransportTypeService.GetAllAsSelectList();
            ViewBag.currencies = CurrencyService.GetAllAsSelectList();
            ViewBag.years = YearService.GetAllAsSelectList();

            return View();
        }

        public ActionResult Extended()
        {
            breadcrumbs.Add("#", Resource.SearchExtended);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.autosCountAddedToday = AutoStatisticsService.GetAutosCountAddedToday();

            return View();
        }

        [HttpPost]
        public ActionResult SearchResult(ExtendedSearchVM modelVM)
        {
            ViewBag.autosCountAddedToday = AutoStatisticsService.GetAutosCountAddedToday();

            if (modelVM.YearOfIssueFrom != null && modelVM.YearOfIssueFrom > 0
                && (modelVM.YearOfIssueTo == null || modelVM.YearOfIssueTo == 0))
            {
                int arrLength = DateTime.Now.Year - modelVM.YearOfIssueFrom.Value + 1;
                if (arrLength > 0)
                {
                    modelVM.YearOfIssue = new int[arrLength];
                    for (int i = modelVM.YearOfIssueFrom.Value, j = 0; i <= DateTime.Now.Year; i++, j++)
                        modelVM.YearOfIssue[j] = i;
                }
            }
            else if (modelVM.YearOfIssueTo != null && modelVM.YearOfIssueTo > 0
                && (modelVM.YearOfIssueFrom == null || modelVM.YearOfIssueFrom == 0))
            {
                int arrLength = modelVM.YearOfIssueTo.Value - 1950 + 1;
                if (arrLength > 0)
                {
                    modelVM.YearOfIssue = new int[arrLength];
                    for (int i = modelVM.YearOfIssueTo.Value, j = 0; i >= 1950; i--, j++)
                        modelVM.YearOfIssue[j] = i;
                }
            }
            else if (modelVM.YearOfIssueTo != null && modelVM.YearOfIssueTo > 0
                && modelVM.YearOfIssueFrom != null && modelVM.YearOfIssueFrom > 0)
            {
                int arrLength = modelVM.YearOfIssueTo.Value - modelVM.YearOfIssueFrom.Value + 1;
                if (arrLength > 0)
                {
                    modelVM.YearOfIssue = new int[arrLength];
                    for (int i = modelVM.YearOfIssueFrom.Value, j = 0; i <= modelVM.YearOfIssueTo.Value; i++, j++)
                        modelVM.YearOfIssue[j] = i;
                }
            }

            return View(modelVM);
        }

        [HttpPost]
        public ActionResult GetAutosFromElastic(int draw, int start, int length, ExtendedSearchVM modelVM, bool forHomePage = false, bool related = false, bool forErrorPage = false)
        {
            int recordsFiltered = 0;
            string error = "";
            List<dynamic> autos = new List<dynamic>();
            List<dynamic> auctions = new List<dynamic>();
            List<List<string>> data = new List<List<string>>();

            if (modelVM == null)
                recordsFiltered = 0;
            else
            {
                bool viewingOwnAutos = false;

                if (modelVM.UserID == null)
                    modelVM.StatusID = 2;
                else
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        User user = UserService.GetUserByEmail(User.Identity.Name);
                        if (modelVM.UserID == user.ID)
                            viewingOwnAutos = true;
                        else
                            modelVM.StatusID = 2;
                    }
                    else
                        modelVM.StatusID = 2;
                }
                //*******************************
                //modelVM.UserID = 16;
                //modelVM.StatusID = null; //should be commented
                //*******************************

                List<AutoShortInfoVM> autoModelsVM = new List<AutoShortInfoVM>();
                List<AuctionShortInfoVM> auctionModelsVM = new List<AuctionShortInfoVM>();

                if (modelVM.Type == "auction")
                    auctions = SearchAutoService.ExtendedSearch(start, length, modelVM, out recordsFiltered, out error);
                else
                    autos = SearchAutoService.ExtendedSearch(start, length, modelVM, out recordsFiltered, out error);

                string usdSymbol = CurrencyService.GetByID(1).Symbol;
                string uahSymbol = CurrencyService.GetByID(2).Symbol;

                if (modelVM.Type == "auction")
                {
                    foreach (var item in auctions)
                    {
                        AuctionShortInfoVM auctionShortInfoVM = CreateAuctionShortInfoVM(item, usdSymbol, uahSymbol);
                        auctionModelsVM.Add(auctionShortInfoVM);
                    }
                }
                else
                {
                    foreach (var item in autos)
                    {
                        AutoShortInfoVM autoShortInfoVM = CreateAutoShortInfoVM(item, usdSymbol, uahSymbol);
                        autoModelsVM.Add(autoShortInfoVM);
                    }
                }

                string html = "";
                if (forHomePage)
                {
                    if (modelVM.Type == "auction")
                        html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardHomeMultiple.cshtml", auctionModelsVM, ControllerContext);
                    else
                        html = ViewRenderer.RenderPartialView("~/Views/AutoCardPartial/_AutoCardHomeMultiple.cshtml", autoModelsVM, ControllerContext);
                }
                else if (forErrorPage)
                {
                    html = ViewRenderer.RenderPartialView("~/Views/AutoCardPartial/_AutoCardError.cshtml", autoModelsVM, ControllerContext);
                }
                else if (related)
                {
                    if (modelVM.Type == "auction")
                        html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardRelatedMultiple.cshtml", auctionModelsVM, ControllerContext);
                    else
                        html = ViewRenderer.RenderPartialView("~/Views/AutoCardPartial/_AutoCardRelatedMultiple.cshtml", autoModelsVM, ControllerContext);
                }
                else
                {
                    if (viewingOwnAutos)
                    {
                        if (modelVM.Type == "auction")
                            html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardInCabinetMultiple.cshtml", auctionModelsVM, ControllerContext);
                        else
                            html = ViewRenderer.RenderPartialView("~/Views/AutoCardPartial/_AutoCardInCabinetMultiple.cshtml", autoModelsVM, ControllerContext);
                    }
                    else
                    {
                        if (modelVM.Type == "auction")
                            html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardMultiple.cshtml", auctionModelsVM, ControllerContext);
                        else
                            html = ViewRenderer.RenderPartialView("~/Views/AutoCardPartial/_AutoCardMultiple.cshtml", autoModelsVM, ControllerContext);
                    }
                }

                if (html != "")
                    data.Add(new List<string>() { html });
            }

            return Json(new { draw = draw, recordsFiltered = recordsFiltered, data = data, error = error }/*, JsonRequestBehavior.AllowGet*/);
        }

        private AutoShortInfoVM CreateAutoShortInfoVM(dynamic auto, string usdSymbol, string uahSymbol)
        {
            try
            {
                AutoShortInfoVM modelVM = new AutoShortInfoVM()
                {
                    Make = GetDynamicProperty(auto, "Make"),
                    Model = GetDynamicProperty(auto, "Model"),
                    Status = GetDynamicProperty(auto, "StatusName"),
                    Region = GetDynamicProperty(auto, "Region"),
                    TransmissionType = GetDynamicProperty(auto, "TransmissionType"),
                    FuelType = GetDynamicProperty(auto, "FuelType"),
                    FuelConsumption = GetDynamicProperty(auto, "FuelConsumption"),
                    Description = GetDynamicProperty(auto, "Description"),
                    Modification = GetDynamicProperty(auto, "Modification"),
                    Photo = new AutoPhotoVM()
                    {
                        ID = 0,
                        IsMain = true,
                        //Src = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + GetDynamicProperty(auto, "PhotoUrl")
                        //Src = "/Content/images/car_page_03.jpg"
                    },
                    TSRegistration = GetDynamicProperty(auto, "TSRegistration"),
                };

                int tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "MainPhotoID"), out tmp);
                modelVM.Photo.ID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "PriceUSDSearch"), out tmp);
                modelVM.PriceUSD = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "PriceUAHSearch"), out tmp);
                modelVM.PriceUAH = tmp;

                string priceFormat = XCarsConfiguration.PriceFormat;
                modelVM.PriceUSDStr = modelVM.PriceUSD.Value.ToString(priceFormat) + " " + usdSymbol;
                modelVM.PriceUAHStr = modelVM.PriceUAH.Value.ToString(priceFormat) + " " + uahSymbol;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "StatusID"), out tmp);
                modelVM.StatusID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "AutoID"), out tmp);
                modelVM.ID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "YearOfIssue"), out tmp);
                modelVM.YearOfIssue = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "Probeg"), out tmp);
                modelVM.Probeg = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "Views"), out tmp);
                modelVM.Views = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "InFavorites"), out tmp);
                modelVM.CountOfFavorites = tmp;

                DateTime dtTmp;
                if (DateTime.TryParse(GetDynamicProperty(auto, "DateAppearance"), out dtTmp))
                    modelVM.DateAppearance = dtTmp;

                decimal tmpDecimal = 0;
                decimal.TryParse(GetDynamicProperty(auto, "EngineCapacity"), out tmpDecimal);
                modelVM.EngineCapacity = tmpDecimal;

                string status = Resource.NotPublished;
                if (modelVM.StatusID == 2)
                {
                    status = Resource.Published;
                    if (modelVM.DateAppearance != null)
                        status += " " + modelVM.DateAppearance.Value.ToString("dd.MM.yy");
                }
                modelVM.Status = status;

                return modelVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private AuctionShortInfoVM CreateAuctionShortInfoVM(dynamic auto, string usdSymbol, string uahSymbol)
        {
            try
            {
                AuctionShortInfoVM modelVM = new AuctionShortInfoVM()
                {
                    Description = GetDynamicProperty(auto, "Description"),
                    Status = GetDynamicProperty(auto, "StatusName"),

                    Auto = new AutoShortInfoVM()
                    {
                        Make = GetDynamicProperty(auto, "Make"),
                        Model = GetDynamicProperty(auto, "Model"),
                        //Status = GetDynamicProperty(auto, "StatusName"),
                        Region = GetDynamicProperty(auto, "Region"),
                        TransmissionType = GetDynamicProperty(auto, "TransmissionType"),
                        FuelType = GetDynamicProperty(auto, "FuelType"),
                        FuelConsumption = GetDynamicProperty(auto, "FuelConsumption"),
                        Description = GetDynamicProperty(auto, "Description"),
                        Modification = GetDynamicProperty(auto, "Modification"),
                        Photo = new AutoPhotoVM()
                        {
                            ID = 0,
                            IsMain = true
                        },
                        TSRegistration = GetDynamicProperty(auto, "TSRegistration"),
                    }
                };

                int tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "MainPhotoID"), out tmp);
                modelVM.Auto.Photo.ID = tmp;

                string priceFormat = XCarsConfiguration.PriceFormat;
                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "PriceUSDSearch"), out tmp);
                modelVM.PriceUSDStr = tmp.ToString(priceFormat) + " " + usdSymbol;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "PriceUAHSearch"), out tmp);
                modelVM.PriceUAHStr = tmp.ToString(priceFormat) + " " + uahSymbol;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "StatusID"), out tmp);
                modelVM.StatusID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "ID"), out tmp);
                modelVM.ID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "AutoID"), out tmp);
                modelVM.Auto.ID = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "YearOfIssue"), out tmp);
                modelVM.Auto.YearOfIssue = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "Probeg"), out tmp);
                modelVM.Auto.Probeg = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "Views"), out tmp);
                modelVM.Views = tmp;

                tmp = 0;
                int.TryParse(GetDynamicProperty(auto, "InFavorites"), out tmp);
                modelVM.CountOfFavorites = tmp;

                DateTime dtTmp;
                if (DateTime.TryParse(GetDynamicProperty(auto, "DateCreated"), out dtTmp))
                    modelVM.DateCreated = dtTmp;

                DateTime dtTmp2;
                if (DateTime.TryParse(GetDynamicProperty(auto, "Deadline"), out dtTmp2))
                    modelVM.Deadline = dtTmp2;

                decimal tmpDecimal = 0;
                decimal.TryParse(GetDynamicProperty(auto, "EngineCapacity"), out tmpDecimal);
                modelVM.Auto.EngineCapacity = tmpDecimal;

                string status = Resource.NotPublished;
                if (modelVM.StatusID == 2)
                    status = Resource.Published + " " + modelVM.DateCreated.ToString("dd.MM.yy");
                modelVM.Status = status;

                string timeLeft = "";
                DateTime targetDT = modelVM.Deadline;
                int leftDaysCount = (targetDT - DateTime.Now).Days;
                if (leftDaysCount > 0)
                {
                    int leftHoursCount = (targetDT.AddDays(-1 * leftDaysCount) - DateTime.Now).Hours;
                    timeLeft = leftDaysCount + " " + Resource.DayShort + " " + leftHoursCount + " " + Resource.HoursShort;
                }
                else
                {
                    int leftHoursCount = (targetDT - DateTime.Now).Hours;
                    if (leftHoursCount > 0)
                    {
                        int leftMinutesCount = (targetDT.AddHours(-1 * leftHoursCount) - DateTime.Now).Minutes;
                        timeLeft = leftHoursCount + " " + Resource.HoursShort + " " + leftMinutesCount + " " + Resource.MinutesShort;
                    }
                    else
                    {
                        int leftMinutesCount = (targetDT - DateTime.Now).Minutes;
                        if (leftMinutesCount > 0)
                        {
                            int leftSecondsCount = (targetDT.AddMinutes(-1 * leftMinutesCount) - DateTime.Now).Seconds;
                            timeLeft = leftMinutesCount + " " + Resource.MinutesShort + " " + leftSecondsCount + " " + Resource.MinutesShort;
                        }
                        else
                        {
                            int leftSecondsCount = (targetDT - DateTime.Now).Seconds;
                            timeLeft = leftSecondsCount + " " + Resource.SecondsShort;
                        }
                    }
                }
                modelVM.TimeLeft = timeLeft;

                return modelVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetDynamicProperty(dynamic obj, string nameOfProperty)
        {
            try
            {
                var propertyInfo = obj.GetType().GetProperty(nameOfProperty);
                string value = propertyInfo.GetValue(obj, null);

                return value;
            }
            catch
            {
                return null;
            }
        }

        public ActionResult GetPotentialPositions(int autoID, int regionID, int makeID, int modelID, int top)
        {
            return Json(SearchAutoService.GetPotentialPositions(autoID, regionID, makeID, modelID, top), JsonRequestBehavior.AllowGet);
        }
    }
}