using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IUserConnectionService : IBaseService<UserConnection>
    {
        void Create(UserConnection model);
        UserConnection GetByCnnID(string connectionID);
        void Delete(string connectionID);
        void ClearAll();
    }
}
