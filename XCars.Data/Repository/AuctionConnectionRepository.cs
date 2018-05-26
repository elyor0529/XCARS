using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AuctionConnectionRepository : RepositoryBase<AuctionConnection>, IAuctionConnectionRepository
    {
        public AuctionConnectionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}