using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class SiteSettingsService : BaseService<SiteSetting>, ISiteSettingsService
    {
        public SiteSettingsService(ISiteSettingsRepository siteSettingsRepository,
                                   IUnitOfWork unitOfWork)
            : base(siteSettingsRepository, unitOfWork)
        {
        }

        public SiteSetting GetByKey(string key)
        {
            return this._repository.Get(s => s.Keyy == key);
        }

        public void Create(SiteSetting model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Edit(SiteSetting model)
        {
            this._repository.Update(model);
            Save();
        }
    }
}
