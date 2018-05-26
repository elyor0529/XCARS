using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AuctionFavoriteRepository : RepositoryBase<AuctionFavorite>, IAuctionFavoriteRepository
    {
        public AuctionFavoriteRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}