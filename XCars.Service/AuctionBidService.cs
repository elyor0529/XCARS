using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionBidService : BaseService<AuctionBid>, IAuctionBidService
    {
        public AuctionBidService(IAuctionBidRepository auctionBidRepository, IUnitOfWork unitOfWork)
            : base(auctionBidRepository, unitOfWork)
        {
        }

        public void Delete(AuctionBid model)
        {
            this._repository.Delete(model);
            Save();
        }
    }
}
