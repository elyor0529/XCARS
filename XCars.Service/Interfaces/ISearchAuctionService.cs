using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCars.Service.Interfaces
{
    public interface ISearchAuctionService
    {
        List<dynamic> ExtendedSearch(int start, int length, dynamic searchVM, out int recordsFiltered, out string error);
    }
}