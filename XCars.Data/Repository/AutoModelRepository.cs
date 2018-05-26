using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoModelRepository : RepositoryBase<AutoModel>, IAutoModelRepository
    {
        public AutoModelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}