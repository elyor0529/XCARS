using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoFavoriteService : BaseService<AutoFavorite>, IAutoFavoriteService
    {
        public AutoFavoriteService(IAutoFavoriteRepository autoFavoriteRepository,
                            IUnitOfWork unitOfWork)
            : base(autoFavoriteRepository, unitOfWork)
        {
        }

        public void Create(AutoFavorite model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Delete(AutoFavorite model)
        {
            this._repository.Delete(model);
            Save();
        }

        public int GetCountOfUserFavorites(User user)
        {
            return user.AutoFavorites.Where(f => f.Auto.StatusID == 2 && f.Auto.DateExpires > DateTime.Now).Count();
        }
    }
}
