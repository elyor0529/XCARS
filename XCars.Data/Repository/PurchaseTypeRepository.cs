using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class PurchaseTypeRepository : RepositoryBase<PurchaseType>, IPurchaseTypeRepository
    {
        public PurchaseTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}