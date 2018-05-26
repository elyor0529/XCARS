using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoStateRepository : RepositoryBase<AutoState>, IAutoStateRepository
    {
        public AutoStateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}