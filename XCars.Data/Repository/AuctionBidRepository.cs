using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AuctionBidRepository : RepositoryBase<AuctionBid>, IAuctionBidRepository
    {
        public AuctionBidRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}