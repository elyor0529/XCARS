using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using XCars.Model;
using XCars.Resourses;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    public class AutoStatisticsController : Controller
    {
        public IAutoStatisticsService AutoStatisticsService { get; set; }
        public IUserService UserService { get; set; }

        public AutoStatisticsController()
        { }

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNumberOfAutosGroupedByMake()
        {
            var ctrl = new Apis.AutoStatisticsController(AutoStatisticsService, UserService);
            var response = ctrl.GetNumberOfAutosGroupedByMake() as OkNegotiatedContentResult<List<object>>;

            return Json(new { result = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserAutosNumberGroupedByStatus()
        {
            var ctrl = new Apis.AutoStatisticsController(AutoStatisticsService, UserService);
            var response = ctrl.GetUserAutosNumberGroupedByStatus() as OkNegotiatedContentResult<List<object>>;

            return Json(new { result = response.Content }, JsonRequestBehavior.AllowGet);
        }
    }
}