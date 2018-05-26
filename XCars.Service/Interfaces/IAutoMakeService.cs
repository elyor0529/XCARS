using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoMakeService : IBaseService<AutoMake>
    {
        void Create(AutoMake model);
        void Edit(AutoMake model);
        void Delete(AutoMake model);
        void DeleteUnused();
        void CreateMany(List<AutoMake> newMakes);

        List<SelectListItem> GetAllAsSelectList(int selected = 0);

        IEnumerable<AutoMake> Get();
        List<SelectListItem> GetAsSelectList(int selected = 0);

        List<SelectListItem> GetAsSelectListMultiple(int[] selected);
    }
}
