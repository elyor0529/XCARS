using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IContactsRequestService : IBaseService<ContactsRequest>
    {
        void Create(ContactsRequest request);
    }
}
