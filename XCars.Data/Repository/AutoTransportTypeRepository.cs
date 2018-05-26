using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoTransportTypeRepository : RepositoryBase<AutoTransportType>, IAutoTransportTypeRepository
    {
        public AutoTransportTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
