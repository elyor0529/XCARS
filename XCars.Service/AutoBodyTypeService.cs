using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoBodyTypeService : BaseService<AutoBodyType>, IAutoBodyTypeService
    {
        public AutoBodyTypeService(IAutoBodyTypeRepository autoBodyTypeRepository, IUnitOfWork unitOfWork)
            : base(autoBodyTypeRepository, unitOfWork)
        {
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

        public IEnumerable<AutoBodyType> Get(int transportTypeID = 0)
        {
            IQueryable<AutoBodyType> bodies = this._repository.GetAny();
            if (transportTypeID > 0)
                bodies = bodies.Where(item => item.TransportTypeID == transportTypeID);

            return bodies;
        }

        public List<SelectListItem> GetAsSelectList(int transportTypeID = 0, int selected = 0)
        {
            return Get(transportTypeID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public IEnumerable<AutoBodyType> Get(int[] transportTypeID)
        {
            IQueryable<AutoBodyType> bodies = this._repository.GetAny();
            if (transportTypeID != null && transportTypeID.Length > 0)
                bodies = bodies.Where(item => transportTypeID.Contains(item.TransportTypeID));

            return bodies;
        }

        public List<SelectListItem> GetAsSelectListMultiple(int[] transportTypeID, int[] selected)
        {
            return Get(transportTypeID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false
            }).ToList();
        }
    }
}
