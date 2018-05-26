using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
            : base(cityRepository, unitOfWork)
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

        public IEnumerable<City> Get(int regionID = 0)
        {
            IQueryable<City> cities = this._repository.GetAny();

            if (regionID > 0)
                cities = cities.Where(c => c.RegionID == regionID);

            return cities;
        }

        public List<SelectListItem> GetAsSelectList(int regionID = 0, int selected = 0)
        {
            return Get(regionID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public IEnumerable<City> Get(int[] regionID)
        {
            IQueryable<City> cities = this._repository.GetAny();

            if (regionID != null && regionID.Length > 0)
                cities = cities.Where(item => regionID.Contains(item.RegionID));

            return cities;
        }

        public List<SelectListItem> GetAsSelectListMultiple(int[] regionID, int[] selected)
        {
            if (regionID != null && regionID.Length == 1 && regionID[0] == 0)
                regionID = null;
            return Get(regionID).Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false
            }).ToList();
        }

        public List<object> GetAsSelectListWithParentIDMultiple(int[] regionID, int[] selected)
        {
            if (regionID != null && regionID.Length == 1 && regionID[0] == 0)
                regionID = null;
            List<object> cities = new List<object>();
            cities.AddRange(Get(regionID).Select(item => new
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false,
                ParentID = item.RegionID
            }).OrderBy(item => item.ParentID));

            return cities;
        }
    }
}
