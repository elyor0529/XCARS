using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoFavoriteRepository : RepositoryBase<AutoFavorite>, IAutoFavoriteRepository
    {
        public AutoFavoriteRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}