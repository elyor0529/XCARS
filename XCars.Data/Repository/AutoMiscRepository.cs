using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoMiscRepository : RepositoryBase<AutoMisc>, IAutoMiscRepository
    {
        public AutoMiscRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}