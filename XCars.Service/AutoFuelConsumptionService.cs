using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoFuelConsumptionService : IAutoFuelConsumptionService
    {
        public IEnumerable<decimal> GetAll()
        {
            decimal min = 5.00M;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoFuelConsumptionMin, out min);

            decimal max = 20.00M;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoFuelConsumptionMax, out max);

            decimal step = 0.1M;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoFuelConsumptionStep, out step);

            List<decimal> values = new List<decimal>();
            for (decimal i = min; i <= max; i = i + step)
            {
                values.Add(i);
            }

            return values;
        }

        public List<SelectListItem> GetAllAsSelectList(int selected = 0)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ToString(),
                Text = item.ToString(),
                Selected = (item == selected) ? true : false
            }).ToList();
        }
    }
}
