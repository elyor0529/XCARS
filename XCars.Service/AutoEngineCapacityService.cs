using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoEngineCapacityService : IAutoEngineCapacityService
    {
        public IEnumerable<decimal> GetAll()
        {
            decimal min = 50;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoEngineCapacityMin, out min);

            decimal max = 300;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoEngineCapacityMax, out max);

            decimal step = 1;
            decimal.TryParse(XCars.Common.XCarsConfiguration.AutoEngineCapacityStep, out step);

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
