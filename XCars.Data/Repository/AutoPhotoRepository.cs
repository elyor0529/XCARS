using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoPhotoRepository : RepositoryBase<AutoPhoto>, IAutoPhotoRepository
    {
        public AutoPhotoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}