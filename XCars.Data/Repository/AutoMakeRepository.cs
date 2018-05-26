using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoMakeRepository : RepositoryBase<AutoMake>, IAutoMakeRepository
    {
        public AutoMakeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}