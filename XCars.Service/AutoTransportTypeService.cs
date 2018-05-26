using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoTransportTypeService : BaseService<AutoTransportType>, IAutoTransportTypeService
    {
        public IAutoMakeRepository AutoMakeRepository { get; set; }

        public AutoTransportTypeService(IAutoTransportTypeRepository autoTransportTypeRepository, 
                                        IUnitOfWork unitOfWork)
            : base(autoTransportTypeRepository, unitOfWork)
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

        //public IEnumerable<AutoTransportType> GetByMakeID(int makeID)
        //{
        //    IEnumerable<AutoTransportType> transportTypes = this._repository.GetAll();
        //    if (makeID > 0)
        //        transportTypes = transportTypes.Where(item => item.AutoBodyTypes.FirstOrDefault(body => body.AutoModels.FirstOrDefault(m => m.MakeID == makeID) != null) != null);

        //    return transportTypes;
        //}

        //public List<SelectListItem> GetByMakeIDAsSelectedList(int makeID, int selected = 0)
        //{
        //    return GetByMakeID(makeID).Select(item => new SelectListItem()
        //    {
        //        Value = item.ID.ToString(),
        //        Text = item.Name,
        //        Selected = (item.ID == selected) ? true : false
        //    }).ToList();
        //}
    }
}
