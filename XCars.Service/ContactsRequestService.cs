using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class ContactsRequestService : BaseService<ContactsRequest>, IContactsRequestService
    {
        public IFileManager FileManager { get; set; }

        public ContactsRequestService(IContactsRequestRepository contactsRequestRepository, IUnitOfWork unitOfWork)
            : base(contactsRequestRepository, unitOfWork)
        {
        }

        public void Create(ContactsRequest request)
        {
            this._repository.Add(request);
            Save();
        }
    }
}
