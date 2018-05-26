using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionStatisticsService : IAuctionStatisticsService
    {
        public IAuctionService AuctionService { get; set; }
        //public IAutoMakeService AutoMakeService { get; set; }

        public int GetAuctionsCountAddedToday()
        {
            DateTime todayStart = DateTime.Now.Date;
            DateTime todayEnd = DateTime.Now.AddDays(1).Date;

            return AuctionService.GetAllActive()
                .Where(a => a.DateCreated >= todayStart
                            && a.DateCreated < todayEnd)
                .Count();
        }

        //public List<object> GetNumberOfAutosGroupedByMake()
        //{
        //    List<object> result = new List<object>();
        //    var autos = AutoService.GetAllPublished()
        //        .GroupBy(item => new
        //        {
        //            MakeID = item.MakeID
        //        })
        //        .Select(item => new
        //        {
        //            MakeID = item.Key.MakeID,
        //            MakeName = item.Max(t => t.AutoMake.Name),
        //            AutosNumber = item.Count()
        //        })
        //        .OrderBy(item => item.MakeName);

        //    foreach (var item in autos)
        //    {
        //        result.Add(item);
        //    }

        //    return result;
        //}

        public List<object> GetUserAuctionsNumberGroupedByStatus(User user)
        {
            List<object> result = new List<object>();
            if (user != null)
            {
                var auctions = AuctionService.GetAll().Where(a => a.Auto.UserID == user.ID)
                .GroupBy(item => new
                {
                    StatusID = item.StatusID
                })
                .Select(item => new
                {
                    StatusID = item.Key.StatusID,
                    AuctionsNumber = item.Count()
                })
                .OrderBy(item => item.StatusID);

                foreach (var item in auctions)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
