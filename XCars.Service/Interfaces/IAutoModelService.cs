using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoModelService : IBaseService<AutoModel>
    {
        void Create(AutoModel model);
        void Edit(AutoModel model);
        void Delete(AutoModel model);

        void DeleteUnused();
        void CreateMany(List<AutoModel> newModels);

        List<SelectListItem> GetAllAsSelectList(int selected = 0);

        IEnumerable<AutoModel> Get(int makeID = 0);
        List<SelectListItem> GetAsSelectList(int makeID = 0, int selected = 0);

        IEnumerable<AutoModel> Get(int[] makeID);
        List<SelectListItem> GetAsSelectListMultiple(int[] makeID, int[] selected);

        List<object> GetAsSelectListWithParentIDMultiple(int[] makeID, int[] selected);
    }
}
