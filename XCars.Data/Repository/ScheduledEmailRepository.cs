using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class ScheduledEmailRepository : RepositoryBase<ScheduledEmail>, IScheduledEmailRepository
    {
        public ScheduledEmailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
