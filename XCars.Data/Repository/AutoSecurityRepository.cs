using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoSecurityRepository : RepositoryBase<AutoSecurity>, IAutoSecurityRepository
    {
        public AutoSecurityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}