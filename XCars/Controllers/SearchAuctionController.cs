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
    public class SearchAuctionController : Controller
    {
        public IUserService UserService { get; set; }
        public ISearchAuctionService SearchAuctionService { get; set; }
        //public IFileManager FileManager { get; set; }
        public ICityService CityService { get; set; }
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IYearService YearService { get; set; }

        // GET: SearchAuction
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAuctionsFromElastic(int draw, int start, int length, ExtendedSearchVM modelVM, bool forHomePage = false, bool related = false)
        {
            int recordsFiltered = 0;
            string error = "";
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
                //modelVM.StateID = null; //should be commented
                //*******************************

                List<AuctionShortInfoVM> auctionModelsVM = new List<AuctionShortInfoVM>();

                auctions = SearchAuctionService.ExtendedSearch(start, length, modelVM, out recordsFiltered, out error);

                string usdSymbol = CurrencyService.GetByID(1).Symbol;
                string uahSymbol = CurrencyService.GetByID(2).Symbol;
                foreach (var item in auctions)
                {
                    AuctionShortInfoVM auctionShortInfoVM = CreateAuctionShortInfoVM(item, usdSymbol, uahSymbol);
                    auctionModelsVM.Add(auctionShortInfoVM);
                }

                string html = "";
                if (forHomePage)
                    html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardHomeMultiple.cshtml", auctionModelsVM, ControllerContext);
                else if (related)
                {
                    html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardRelatedMultiple.cshtml", auctionModelsVM, ControllerContext);
                }
                else
                {
                    if (viewingOwnAutos)
                        html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardInCabinetMultiple.cshtml", auctionModelsVM, ControllerContext);
                    else
                        html = ViewRenderer.RenderPartialView("~/Views/AuctionCardPartial/_AuctionCardMultiple.cshtml", auctionModelsVM, ControllerContext);
                }

                if (html != "")
                    data.Add(new List<string>() { html });
            }

            return Json(new { draw = draw, recordsFiltered = recordsFiltered, data = data, error = error }/*, JsonRequestBehavior.AllowGet*/);
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
    }
}