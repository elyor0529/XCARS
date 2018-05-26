using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoMiscService : IBaseService<AutoMisc>
    {
        List<SelectListItem> GetAllAsSelectList(int[] selected);
    }
}
