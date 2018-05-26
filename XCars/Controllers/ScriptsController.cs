using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCars.Service.Interfaces;
using ExcelDataReader;
using System.Globalization;

namespace XCars.Controllers
{
    [Authorize]
    public class ScriptsController : Controller
    {
        public IScriptsService ScriptsService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            if (id != 345634)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        public ActionResult Update(HttpPostedFileBase excel, int typeID = 0)
        {
            //return View();
            try
            {
                Stream stream = excel.InputStream;
                IExcelDataReader reader = null;

                if (excel.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (excel.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    ModelState.AddModelError("File", "This file format is not supported");
                    return View();
                }

                if (typeID == 1)
                    ScriptsService.UpdateMakes(reader);
                else if (typeID == 2)
                    ScriptsService.UpdateModels(reader);
                else if (typeID == 3)
                    ScriptsService.UpdateBothMakeAndModel(reader);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View();
        }
    }
}