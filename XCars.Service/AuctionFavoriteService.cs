using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionFavoriteService : BaseService<AuctionFavorite>, IAuctionFavoriteService
    {
        public AuctionFavoriteService(IAuctionFavoriteRepository auctionFavoriteRepository,
                            IUnitOfWork unitOfWork)
            : base(auctionFavoriteRepository, unitOfWork)
        {
        }

        public void Create(AuctionFavorite model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Delete(AuctionFavorite model)
        {
            this._repository.Delete(model);
            Save();
        }

        public int GetCountOfUserFavorites(User user)
        {
            return user.AuctionFavorites.Where(f => f.Auction.StatusID == 2 && f.Auction.Deadline > DateTime.Now).Count();
        }
    }
}
