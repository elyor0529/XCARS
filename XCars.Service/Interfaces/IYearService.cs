using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IYearService
    {
        IEnumerable<int> GetAll();
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
        List<SelectListItem> GetAllAsSelectListMultiple(int[] selected);
    }
}
