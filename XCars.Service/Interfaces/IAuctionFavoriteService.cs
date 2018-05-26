using XCars.Model;
using System.Collections.Generic;

namespace XCars.Service.Interfaces
{
    public interface IAuctionFavoriteService : IBaseService<AuctionFavorite>
    {
        void Create(AuctionFavorite model);
        void Delete(AuctionFavorite model);
        int GetCountOfUserFavorites(User user);
    }
}