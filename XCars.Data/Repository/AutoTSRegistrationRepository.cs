using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoTSRegistrationRepository : RepositoryBase<AutoTSRegistration>, IAutoTSRegistrationRepository
    {
        public AutoTSRegistrationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}