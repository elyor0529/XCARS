using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IAutoEngineCapacityService
    {
        IEnumerable<decimal> GetAll();
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}
