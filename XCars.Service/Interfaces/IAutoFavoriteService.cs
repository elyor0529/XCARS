using XCars.Model;
using System.Collections.Generic;

namespace XCars.Service.Interfaces
{
    public interface IAutoFavoriteService : IBaseService<AutoFavorite>
    {
        void Create(AutoFavorite model);
        void Delete(AutoFavorite model);
        int GetCountOfUserFavorites(User user);
    }
}