using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IHangfireService
    {
        string CreateJobForAuction(Auction auction);
        void FinishAuction(int id);
        string CreateJobForAuctionDeletion(Auction auction);
        void DeleteAuction(int id);
        string CreateJobForAuto(Auto auto);
        void FinishAuto(int id);
        void CancelJob(string jobID);
    }
}
