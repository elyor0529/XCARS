using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoStatusService : IBaseService<AutoStatu>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}