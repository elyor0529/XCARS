using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoTransmissionTypeRepository : RepositoryBase<AutoTransmissionType>, IAutoTransmissionTypeRepository
    {
        public AutoTransmissionTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}