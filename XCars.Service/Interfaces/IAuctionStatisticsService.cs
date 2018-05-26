using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionStatisticsService
    {
        int GetAuctionsCountAddedToday();

        //List<object> GetNumberOfAutosGroupedByMake();

        List<object> GetUserAuctionsNumberGroupedByStatus(User user);
    }
}