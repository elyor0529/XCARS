using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoBodyTypeRepository : RepositoryBase<AutoBodyType>, IAutoBodyTypeRepository
    {
        public AutoBodyTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}