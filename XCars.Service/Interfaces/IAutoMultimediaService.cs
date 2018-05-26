using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoMultimediaService : IBaseService<AutoMultimedia>
    {
        List<SelectListItem> GetAllAsSelectList(int[] selected);
    }
}
