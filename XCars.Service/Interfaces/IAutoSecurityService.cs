using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoSecurityService : IBaseService<AutoSecurity>
    {
        List<SelectListItem> GetAllAsSelectList(int[] selected);
    }
}
