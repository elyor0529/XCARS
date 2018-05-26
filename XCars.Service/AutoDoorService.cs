using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoDoorService : IAutoDoorService
    {
        public IEnumerable<int> Get()
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i < 6; i++)
            {
                numbers.Add(i);
            }

            return numbers;
        }

        public List<SelectListItem> GetAsSelectList(int selected = 0)
        {
            return Get().Select(item => new SelectListItem()
            {
                Value = item.ToString(),
                Text = item.ToString(),
                Selected = (item == selected) ? true : false
            }).ToList();
        }
    }
}
