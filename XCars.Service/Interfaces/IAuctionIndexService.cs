using System.Collections.Generic;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionIndexService : IBaseService<Auction>
    {
        void AddToIndex(Auction auto);
        void UpdateIndex(Auction auto);
        void DeleteFromIndex(Auction auto);
        object GetSingleShortFromIndex(int id);
        IEnumerable<object> GetManyFromIndex();
        void UpdatePriceForAllAuctionsInIndex(double rate);
    }
}