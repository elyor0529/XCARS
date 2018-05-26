using XCars.Model;
using System.Collections.Generic;

namespace XCars.Service.Interfaces
{
    public interface IAuctionService : IBaseService<Auction>
    {
        Auction GetUnactive(int id);
        void Create(Auction model);
        void Edit(Auction model);
        void Delete(Auction model);
        void Finish(Auction model, bool finishedManually = false);
        Auction GetActiveByID(int id);
        IEnumerable<Auction> GetByUserID(int userID);
        IEnumerable<Auction> GetAllActive();
        Dictionary<int, int> GetRecommendedPrice(int? priceUSD, int? priceUAH);
    }
}
