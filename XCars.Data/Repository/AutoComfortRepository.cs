using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoComfortRepository : RepositoryBase<AutoComfort>, IAutoComfortRepository
    {
        public AutoComfortRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}