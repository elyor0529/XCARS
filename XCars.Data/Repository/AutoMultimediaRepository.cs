using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoMultimediaRepository : RepositoryBase<AutoMultimedia>, IAutoMultimediaRepository
    {
        public AutoMultimediaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}