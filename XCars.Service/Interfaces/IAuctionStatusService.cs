using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionStatusService : IBaseService<AuctionStatu>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
    }
}