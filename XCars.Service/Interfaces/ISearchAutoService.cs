using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCars.Service.Interfaces
{
    public interface ISearchAutoService
    {
        List<dynamic> ExtendedSearch(int start, int length, dynamic searchVM, out int recordsFiltered, out string error);
        //List<object> Search(int start, int length, int? statusID, string searchText, int? userID,
        //                            int periodFilterHours, DateTime? periodFilterStart, DateTime? periodFilterEnd,
        //                            int? TransportTypeID, int? MakeID, int? ModelID, int? RegionID,
        //                            int? yearFrom, int? yearTo, int? priceFrom, int? priceTo, int? CurrencyID,
        //                            out int recordsFiltered, out string error);
        int[] GetPotentialPositions(int autoID, int regionID, int makeID, int modelID, int top = 0);
    }
}