using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        //public IEnumerable<User> GetUsersByPage(int currentPage, int noOfRecords, string sortBy, string filterBy)
        //{
        //    var skipEvents = noOfRecords * currentPage;

        //    var users = this.DataContext.Users.OrderBy(o => o.ID).Skip(skipEvents).Take(noOfRecords);

        //    return users.ToList();
        //}
    }
}
