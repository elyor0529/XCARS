using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.EmailServiceReference;
using XCars.Service.Interfaces;
using XCars.Common;
using Hangfire.Server;
using Hangfire.Console;
using System.Threading.Tasks;

namespace XCars.Service
{
    public class HangfireService : IHangfireService
    {
        public IAuctionService AuctionService { get; set; }
        public IAutoService AutoService { get; set; }

        public HangfireService()
        {
        }

        public string CreateJobForAuction(Auction auction)
        {
            //-------------------
            //DateTimeOffset dateOffset = new DateTimeOffset(DateTime.Now);
            //dateOffset = new DateTimeOffset(auction.Deadline);
            //-------------------
            //string jobID = BackgroundJob.Enqueue(() => Task.CompletedTask);
            string jobID = BackgroundJob.Schedule(() => FinishAuction(auction.ID), new DateTimeOffset(auction.Deadline));

            return jobID;
        }

        public void FinishAuction(int id)
        {
            try
            {
                Auction auction = AuctionService.GetByID(id);
                AuctionService.Finish(auction);
            }
            catch (Exception ex)
            { }
        }

        public string CreateJobForAuctionDeletion(Auction auction)
        {
            int hours = 6;
            string tmp = $"{XCarsConfiguration.XHoursRemaingToDraftAuctionDeletion}";
            int.TryParse(tmp, out hours);

            //hours = 1;
            //DateTimeOffset dateOffset = new DateTimeOffset(DateTime.Now.AddMinutes(hours));

            DateTimeOffset dateOffset = new DateTimeOffset(DateTime.Now.AddHours(hours));
            string jobID = BackgroundJob.Schedule(() => DeleteAuction(auction.ID), dateOffset);

            return jobID;
        }

        public void DeleteAuction(int id)
        {
            try
            {
                Auction auction = AuctionService.GetByID(id);
                if (auction.StatusID == 1)
                    AuctionService.Delete(auction);
            }
            catch (Exception ex)
            { }
        }

        public string CreateJobForAuto(Auto auto)
        {
            string jobID = null;
            if (auto.DateExpires != null)
                jobID = BackgroundJob.Schedule(() => FinishAuto(auto.ID), new DateTimeOffset((DateTime)auto.DateExpires));

            return jobID;
        }

        public void FinishAuto(int id)
        {
            try
            {
                Auto auto = AutoService.GetByID(id);
                AutoService.MoveToArchives(auto);
            }
            catch (Exception ex)
            { }
        }

        public void CancelJob(string jobID)
        {
            if (!string.IsNullOrWhiteSpace(jobID))
            {
                try
                {
                    BackgroundJob.Delete(jobID);
                }
                catch
                { }
            }
        }
    }
}
