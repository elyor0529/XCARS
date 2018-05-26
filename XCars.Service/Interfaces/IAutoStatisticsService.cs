using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoStatisticsService
    {
        int GetAutosCountAddedToday();

        List<object> GetNumberOfAutosGroupedByMake();

        List<object> GetUserAutosNumberGroupedByStatus(User user);
    }
}
