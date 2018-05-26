using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class UserConnectionRepository : RepositoryBase<UserConnection>, IUserConnectionRepository
    {
        public UserConnectionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}