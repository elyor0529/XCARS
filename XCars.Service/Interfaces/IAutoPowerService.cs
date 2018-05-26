using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IAutoPowerService
    {
        IEnumerable<int> GetAll();
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}