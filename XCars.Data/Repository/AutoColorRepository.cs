using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoColorRepository : RepositoryBase<AutoColor>, IAutoColorRepository
    {
        public AutoColorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}