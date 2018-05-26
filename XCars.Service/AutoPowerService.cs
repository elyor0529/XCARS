using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoPowerService : IAutoPowerService
    {
        public IEnumerable<int> GetAll()
        {
            int min = 50;
            int.TryParse(XCars.Common.XCarsConfiguration.AutoPowerMin, out min);

            int max = 300;
            int.TryParse(XCars.Common.XCarsConfiguration.AutoPowerMax, out max);

            int step = 1;
            int.TryParse(XCars.Common.XCarsConfiguration.AutoPowerStep, out step);

            List<int> values = new List<int>();
            for (int i = min; i <= max; i = i + step)
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
