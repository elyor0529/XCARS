using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoRepository : RepositoryBase<Auto>, IAutoRepository
    {
        public AutoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}