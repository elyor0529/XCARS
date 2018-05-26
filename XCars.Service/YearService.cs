using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class YearService : IYearService
    {
        public IEnumerable<int> GetAll()
        {
            int min = 1950;
            int.TryParse(XCars.Common.XCarsConfiguration.YearMin, out min);

            List<int> years = new List<int>();
            for (int i = DateTime.Now.Year; i >= min; i--)
            {
                years.Add(i);
            }

            return years;
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

        public List<SelectListItem> GetAllAsSelectListMultiple(int[] selected)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ToString(),
                Text = item.ToString(),
                Selected = (selected != null && selected.Contains(item)) ? true : false,
            }).ToList();
        }
    }
}
