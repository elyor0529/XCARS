using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoTransportTypeService : IBaseService<AutoTransportType>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
        //IEnumerable<AutoTransportType> GetByMakeID(int makeID);
        //List<SelectListItem> GetByMakeIDAsSelectedList(int makeID, int selected = 0);
    }
}
