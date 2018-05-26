using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IAutoFuelConsumptionService
    {
        IEnumerable<decimal> GetAll();
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}
