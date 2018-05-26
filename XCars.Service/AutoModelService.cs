using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoModelService : BaseService<AutoModel>, IAutoModelService
    {
        public AutoModelService(IAutoModelRepository autoModelRepository, IUnitOfWork unitOfWork)
            : base(autoModelRepository, unitOfWork)
        {
        }

        public void Create(AutoModel model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Edit(AutoModel model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(AutoModel model)
        {
            this._repository.Delete(model);
            Save();
        }

        //unused makes and models
        public void DeleteUnused()
        {
            IEnumerable<AutoModel> unusedModels = _repository.GetMany(m => m.Autoes.Count == 0);
            foreach (var item in unusedModels)
                _repository.Delete(item);
            Save();
        }

        public void CreateMany(List<AutoModel> newModels)
        {
            IEnumerable<AutoModel> existingModels = GetAll();
            List<int> existingModelsIDs = existingModels.Select(make => make.ID).ToList();
            newModels = newModels.Where(make => !existingModelsIDs.Contains(make.ID)).ToList();

            foreach (var item in newModels)
                _repository.Add(item);
            Save();
        }

        public List<SelectListItem> GetAllAsSelectList(int selected = 0)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public IEnumerable<AutoModel> Get(int makeID = 0)
        {
            IQueryable<AutoModel> models = this._repository.GetAny();

            if (makeID > 0)
                models = models.Where(m => m.MakeID == makeID);

            return models;
        }

        public List<SelectListItem> GetAsSelectList(int makeID = 0, int selected = 0)
        {
            return Get(makeID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public IEnumerable<AutoModel> Get(int[] makeID)
        {
            IQueryable<AutoModel> models = this._repository.GetAny();
            if (makeID != null && makeID.Length > 0)
                models = models.Where(item => makeID.Contains(item.MakeID));

            return models;
        }

        public List<SelectListItem> GetAsSelectListMultiple(int[] makeID, int[] selected)
        {
            return Get(makeID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false
            }).ToList();
        }

        public List<object> GetAsSelectListWithParentIDMultiple(int[] makeID, int[] selected)
        {
            if (makeID != null && makeID.Length == 1 && makeID[0] == 0)
                makeID = null;
            List<object> models = new List<object>();
            models.AddRange(Get(makeID).Select(item => new
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false,
                ParentID = item.MakeID
            }).OrderBy(item => item.ParentID));
            
            return models;
        }
    }
}
