using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoFuelTypeRepository : RepositoryBase<AutoFuelType>, IAutoFuelTypeRepository
    {
        public AutoFuelTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}