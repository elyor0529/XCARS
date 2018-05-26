using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AuctionPhotoRepository : RepositoryBase<AuctionPhoto>, IAuctionPhotoRepository
    {
        public AuctionPhotoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}