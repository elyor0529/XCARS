using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoDriveTypeService : IBaseService<AutoDriveType>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
        List<SelectListItem> GetAllAsSelectListMultiple(int[] selected);
    }
}
