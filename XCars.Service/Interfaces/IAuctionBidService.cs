using System.Collections.Generic;
using System.Web;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionBidService : IBaseService<AuctionBid>
    {
        void Delete(AuctionBid model);
    }
}