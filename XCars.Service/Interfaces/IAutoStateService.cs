using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoStateService : IBaseService<AutoState>
    {
        List<SelectListItem> GetAllAsSelectList(int[] selected);
    }
}
