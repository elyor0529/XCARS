using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface ISiteSettingsService : IBaseService<SiteSetting>
    {
        SiteSetting GetByKey(string key);

        void Create(SiteSetting model);

        void Edit(SiteSetting model);
    }
}
