using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
        IEnumerable<City> Get(int regionID = 0);
        List<SelectListItem> GetAsSelectList(int regionID = 0, int selected = 0);

        IEnumerable<City> Get(int[] regionID);
        List<SelectListItem> GetAsSelectListMultiple(int[] regionID, int[] selected);
        List<object> GetAsSelectListWithParentIDMultiple(int[] regionID, int[] selected);
    }
}
