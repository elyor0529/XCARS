using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCars.Resourses;

namespace XCars.Controllers
{
    [Authorize]
    public class MyCabinetController : Controller
    {
        Dictionary<string, string> breadcrumbs = new Dictionary<string, string>();

        public MyCabinetController()
        {
            breadcrumbs.Add("/", Resource.Main);
        }

        // GET: MyCabinet
        public ActionResult Index()
        {
            breadcrumbs.Add("#", Resource.MyCabinet);
            ViewBag.breadcrumbs = breadcrumbs;

            return View();
        }
    }
}