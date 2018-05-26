using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoBodyTypeService : IBaseService<AutoBodyType>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);

        //IEnumerable<AutoBodyType> GetByTransportTypeID(int transportTypeID);
        //List<SelectListItem> GetByTransportTypeIDAsSelectList(int transportTypeID, int selected = 0);

        IEnumerable<AutoBodyType> Get(int transportTypeID = 0);
        List<SelectListItem> GetAsSelectList(int transportTypeID = 0, int selected = 0);

        IEnumerable<AutoBodyType> Get(int[] transportTypeID);
        List<SelectListItem> GetAsSelectListMultiple(int[] transportTypeID, int[] selected);
    }
}
