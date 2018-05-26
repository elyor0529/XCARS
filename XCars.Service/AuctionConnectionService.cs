using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionConnectionService : BaseService<AuctionConnection>, IAuctionConnectionService
    {
        public AuctionConnectionService(IAuctionConnectionRepository auctionConnectionRepository,
                                        IUnitOfWork unitOfWork)
            : base(auctionConnectionRepository, unitOfWork)
        {
        }

        public void Create(AuctionConnection model)
        {
            this._repository.Add(model);
            Save();
        }

        public AuctionConnection GetByCnnID(string connectionID)
        {
            return this._repository.Get(a => a.Connection == connectionID);
        }

        public void Delete(string connectionID)
        {
            AuctionConnection connection = GetByCnnID(connectionID);
            if (connection != null)
            {
                this._repository.Delete(connection);
                Save();
            }
        }

        private void Delete(AuctionConnection connection)
        {
            this._repository.Delete(connection);
            Save();
        }

        public void ClearAll()
        {
            List<AuctionConnection> connections = _repository.GetAll().ToList();
            for (int i = 0; i < connections.Count; i++)
                Delete(connections[i]);
        }
    }
}
