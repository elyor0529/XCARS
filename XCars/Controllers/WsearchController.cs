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
    public class WsearchController : Controller
    {
        public IUserService UserService { get; set; }
        public ISearchAutoService SearchAutoService { get; set; }
        //public IFileManager FileManager { get; set; }
        public ICityService CityService { get; set; }
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IYearService YearService { get; set; }
        public IAutoStatisticsService AutoStatisticsService { get; set; }

        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public WsearchController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: Wsearch
        public ActionResult Index()
        {
            breadcrumbs.Add("#", Resource.SearchExtended);
            ViewBag.breadcrumbs = breadcrumbs;

            ViewBag.autosCountAddedToday = AutoStatisticsService.GetAutosCountAddedToday();

            return View();
        }
    }
}