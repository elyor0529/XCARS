using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}