using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCars.Service;
using XCars.Service.Interfaces;

namespace XCars
{
    public static class SignalRConnections
    {
        public static void ClearAuctionConnections()
        {
            IAuctionConnectionService AuctionConnectionService = DependencyResolver.Current.GetService<IAuctionConnectionService>();
            AuctionConnectionService.ClearAll();
        }

        public static void ClearUserConnections()
        {
            IUserConnectionService UserConnectionService = DependencyResolver.Current.GetService<IUserConnectionService>();
            UserConnectionService.ClearAll();
        }
    }
}