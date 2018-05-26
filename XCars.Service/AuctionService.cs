using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.EmailServiceReference;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionService : BaseService<Auction>, IAuctionService
    {
        //public IHangfireService HangfireService { get; set; }
        public IScheduledEmailService ScheduledEmailService { get; set; }
        public IAuctionPhotoService AuctionPhotoService { get; set; }
        public ICurrencyService CurrencyService { get; set; }
        public IAuctionFavoriteService AuctionFavoriteService { get; set; }
        public IAuctionIndexService AuctionIndexService { get; set; }
        public IAuctionBidService AuctionBidService { get; set; }

        public AuctionService(IAuctionRepository auctionRepository,
                            IUnitOfWork unitOfWork)
            : base(auctionRepository, unitOfWork)
        {
        }

        public Auction GetUnactive(int id)
        {
            return this._repository.Get(a => a.ID == id && a.StatusID != 2);
        }

        public void Create(Auction model)
        {
            this._repository.Add(model);
            Save();

            AuctionIndexService.AddToIndex(model);

            int eligiblePeriodInMinutes = 0;
            string tmp = $"{XCarsConfiguration.XMinutesAuctionFinishEmailEligiblePeriod}";
            int.TryParse(tmp, out eligiblePeriodInMinutes);

            ScheduledEmail email = new ScheduledEmail()
            {
                DateScheduled = DateTime.Now,
                DateDue = model.Deadline,
                //StatusID = 1,
                To = model.Auto.User.Email,
                Subject = "Subject1",
                Body = "Text1",
                ObjectTypeID = 2,
                ObjectID = model.ID
            };
            email.DateEligible = email.DateDue.AddMinutes(eligiblePeriodInMinutes);
            ScheduledEmailService.Create(email);

            int minutes = 0;
            tmp = $"{XCarsConfiguration.XMinutesRemaingToAuctionDeadline}";
            int.TryParse(tmp, out minutes);

            eligiblePeriodInMinutes = 0;
            tmp = $"{XCarsConfiguration.XMinutesRemainingAuctionFinishEmailEligiblePeriod}";
            int.TryParse(tmp, out eligiblePeriodInMinutes);

            ScheduledEmail email2 = new ScheduledEmail()
            {
                DateScheduled = DateTime.Now,
                DateDue = model.Deadline.AddMinutes(-1 * minutes),
                //StatusID = 1,
                To = model.Auto.User.Email,
                Subject = "Subject1",
                Body = "Text1",
                ObjectTypeID = 3,
                ObjectID = model.ID
            };
            email2.DateEligible = email2.DateDue.AddMinutes(eligiblePeriodInMinutes);
            ScheduledEmailService.Create(email2);
        }

        public void Edit(Auction model)
        {
            double currencyRate = CurrencyService.GetCurrencyRate();
            model.PriceUSDSearch = (model.CurrencyID == 1) ? model.StartPrice : (int)(model.StartPrice / currencyRate);
            model.PriceUAHSearch = (model.CurrencyID == 2) ? model.StartPrice : (int)(model.StartPrice * currencyRate);

            this._repository.Update(model);
            Save();

            AuctionIndexService.UpdateIndex(model); 
        }

        public void Delete(Auction model)
        {
            List<AuctionPhoto> photos = model.AuctionPhotoes.ToList();
            foreach (var item in photos)
                AuctionPhotoService.Delete(item.ID);

            List<AuctionBid> bids = model.AuctionBids.ToList();
            foreach (var item in bids)
                AuctionBidService.Delete(item);

            AuctionIndexService.DeleteFromIndex(model);

            this._repository.Delete(model);
            Save();
        }

        public void Finish(Auction model, bool finishedManually = false)
        {
            model.StatusID = 3;
            Edit(model);

            List<AuctionFavorite> favs = model.AuctionFavorites.ToList();
            for (int i = 0; i < favs.Count; i++)
                AuctionFavoriteService.Delete(favs[i]);

            if (finishedManually)
            {
                ScheduledEmailService.CancelScheduledEmails(2, model.ID);
                ScheduledEmailService.CancelScheduledEmails(3, model.ID);
            }
        }

        public Auction GetActiveByID(int id)
        {
            return this._repository.GetById(id);
        }

        public IEnumerable<Auction> GetByUserID(int userID)
        {
            return _repository.GetMany(a => a.Auto.UserID == userID);
        }

        public IEnumerable<Auction> GetAllActive()
        {
            return _repository.GetMany(a => a.StatusID == 2);
        }
         
        //public List<object> GetAllActive()
        //{
        //    List<object> result = new List<object>();
        //    var auctions = _repository.GetMany(a => a.StateID == 2)
        //        .Select(item => new
        //        {
        //            ID = item.ID,
        //            Deadline = item.Deadline
        //        });

        //    foreach (var item in auctions)
        //    {
        //        result.Add(item);
        //    }

        //    return result;
        //}

        public Dictionary<int, int> GetRecommendedPrice(int? priceUSD, int? priceUAH)
        {
            int recommendedPriceUSD = 0;
            int recommendedPriceUAH = 0;
            double currencyRate = CurrencyService.GetCurrencyRate();

            if ((priceUSD != null || priceUAH != null) && currencyRate > 0)
            {
                int price_usd = (priceUSD != null) ? (int)priceUSD : (priceUAH != null) ? (int)priceUAH / (int)currencyRate : 0;

                try
                {
                    string tmp = $"{XCarsConfiguration.RecommendedPriceRates}"; //"0-2999|20;3000-4999|18;5000-9999|15;10000-14999|13;15000-19999|11;20000-0|10"
                    if (!string.IsNullOrWhiteSpace(tmp))
                    {
                        string[] priceRanges = tmp.Split(';');
                        if (priceRanges.Length > 0)
                        {
                            for (int i = 0; i < priceRanges.Length; i++)
                            {
                                string[] tmp2 = priceRanges[i].Split('|');

                                double rate = 0;
                                double.TryParse(tmp2[1], out rate);

                                tmp2 = tmp2[0].Split('-');

                                int downLimit = 0;
                                int.TryParse(tmp2[0], out downLimit);
                                int upLimit = 0;
                                int.TryParse(tmp2[1], out upLimit);

                                if (upLimit > 0 && price_usd >= downLimit && price_usd <= upLimit
                                    || upLimit == 0 && price_usd > downLimit)
                                {
                                    double price_usdD = price_usd;
                                    recommendedPriceUSD = (int)(price_usdD * (100D-rate)/100D);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                { }
            }

            recommendedPriceUAH = (int)(recommendedPriceUSD * currencyRate);

            int ostatok = recommendedPriceUSD % 100;
            if (ostatok != 0)
                recommendedPriceUSD = recommendedPriceUSD - ostatok + 100;
            ostatok = recommendedPriceUAH % 100;
            if (ostatok != 0)
                recommendedPriceUAH = recommendedPriceUAH - ostatok + 100;

            return new Dictionary<int, int>()
            {
                {1,  recommendedPriceUSD},
                {2,  recommendedPriceUAH}
            };
        }
    }
}
