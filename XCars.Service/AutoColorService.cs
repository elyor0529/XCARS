using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoColorService : BaseService<AutoColor>, IAutoColorService
    {
        public AutoColorService(IAutoColorRepository autoColorRepository, IUnitOfWork unitOfWork)
            : base(autoColorRepository, unitOfWork)
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

        public Dictionary<int, string> GetAllWithHexDictionary()
        {
            return GetAll().ToDictionary(k => k.ID, v => v.Hex);
        }
    }
}