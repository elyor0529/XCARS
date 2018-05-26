using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionConnectionService : IBaseService<AuctionConnection>
    {
        void Create(AuctionConnection model);
        AuctionConnection GetByCnnID(string connectionID);
        void Delete(string connectionID);
        void ClearAll();
    }
}