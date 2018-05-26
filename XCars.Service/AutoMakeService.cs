using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoMakeService : BaseService<AutoMake>, IAutoMakeService
    {
        public IAutoBodyTypeRepository AutoBodyTypeRepository { get; set; }

        public AutoMakeService(IAutoMakeRepository autoMakeRepository,
                                IUnitOfWork unitOfWork)
            : base(autoMakeRepository, unitOfWork)
        {
        }

        public void Create(AutoMake model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Edit(AutoMake model)
        {
            this._repository.Update(model);
            Save();
        }

        public void Delete(AutoMake model)
        {
            this._repository.Delete(model);
            Save();
        }

        public void DeleteUnused()
        {
            IEnumerable<AutoMake> unusedMakes = _repository.GetMany(m => m.Autoes.Count == 0);
            foreach (var item in unusedMakes)
                _repository.Delete(item);
            Save();
        }

        public void CreateMany(List<AutoMake> newMakes)
        {
            IEnumerable<AutoMake> existingMakes = GetAll();
            List<int> existingMakeIDs = existingMakes.Select(make => make.ID).ToList();
            newMakes = newMakes.Where(make => !existingMakeIDs.Contains(make.ID)).ToList();

            foreach (var item in newMakes)
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

        public IEnumerable<AutoMake> Get()
        {
            IQueryable<AutoMake> makes = this._repository.GetAny();
            return makes;
        }

        public List<SelectListItem> GetAsSelectList(int selected = 0)
        {
            return Get().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public List<SelectListItem> GetAsSelectListMultiple(int[] selected)
        {
            return Get().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false,
            }).ToList();
        }
    }
}
