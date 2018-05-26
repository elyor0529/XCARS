using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class ContactsRequestRepository : RepositoryBase<ContactsRequest>, IContactsRequestRepository
    {
        public ContactsRequestRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}