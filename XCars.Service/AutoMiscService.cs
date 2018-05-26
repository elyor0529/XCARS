using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoMiscService : BaseService<AutoMisc>, IAutoMiscService
    {
        public AutoMiscService(IAutoMiscRepository autoMiscRepository, IUnitOfWork unitOfWork)
            : base(autoMiscRepository, unitOfWork)
        {
        }

        public List<SelectListItem> GetAllAsSelectList(int[] selected)
        {
            if (selected == null)
                selected = new int[0];

            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected.Contains(item.ID)) ? true : false
            }).ToList();
        }
    }
}
