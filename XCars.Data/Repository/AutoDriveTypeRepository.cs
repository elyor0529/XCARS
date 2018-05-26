using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoDriveTypeRepository : RepositoryBase<AutoDriveType>, IAutoDriveTypeRepository
    {
        public AutoDriveTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}