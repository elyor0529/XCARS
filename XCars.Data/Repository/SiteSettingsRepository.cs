using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class SiteSettingsRepository : RepositoryBase<SiteSetting>, ISiteSettingsRepository
    {
        public SiteSettingsRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}