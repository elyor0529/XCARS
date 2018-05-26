using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoColorService : IBaseService<AutoColor>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);

        Dictionary<int, string> GetAllWithHexDictionary();
    }
}