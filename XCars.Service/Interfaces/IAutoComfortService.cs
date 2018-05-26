using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoComfortService : IBaseService<AutoComfort>
    {
        List<SelectListItem> GetAllAsSelectList(int[] selected);
    }
}
