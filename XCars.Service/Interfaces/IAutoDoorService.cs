using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IAutoDoorService
    {
        IEnumerable<int> Get();
        List<SelectListItem> GetAsSelectList(int selected = 0);
    }
}
