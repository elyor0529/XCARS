using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        //public IAutoService AutoService { get; set; }
        public IAutoStatisticsService AutoStatisticsService { get; set; }

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            ViewBag.autosCountAddedToday = AutoStatisticsService.GetAutosCountAddedToday();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}