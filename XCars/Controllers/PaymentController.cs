using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service.Interfaces;
using XCars.ViewModels;

namespace XCars.Controllers
{
    public class PaymentController : Controller
    {
        public IUserService UserService { get; set; }
        public IOrderService OrderService { get; set; }

        public PaymentController()
        {
        }

        [HttpPost]
        public ActionResult Result(OrderVM orderVM)
        {
            if (OrderService.IsValid(orderVM))
            {
                //предварительный запрос
                if (orderVM.LMI_PREREQUEST == 1)
                    return Content("YES");
                //оповещение об успешном платеже
                else if (orderVM.LMI_SYS_PAYMENT_ID != 0)
                    OrderService.Process(orderVM);

                return Content("NO");
            }
            else
                return Content("NO");
        }
    }
}