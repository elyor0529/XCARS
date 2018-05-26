using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using XCars.Model;
using XCars.Service;
using XCars.ViewModels;
using XCars.Service.Interfaces;
using XCars.Common;
using XCars.Resourses;
using System.Web.Mvc;

namespace XCars.Hubs
{
    public class AuctionHub : Hub
    {
        //public IUserService _userService;
        //public IAuctionConnectionService _auctionConnectionService;

        XCarsEntities db = new XCarsEntities();
        FileManager fileManager = new FileManager();

        //TO-DO
        //Dependency Injection should be used !!!
        //Now I could not do that (04.12.2017)
        //public AuctionHub(IAuctionConnectionService auctionConnectionService, IUserService userService)
        //{
        //    _userService = userService;
        //    _auctionConnectionService = auctionConnectionService;
        //}
        public AuctionHub()
        {

        }

        //Context.User = NULL!!!!!
        //don't know why...
        public void SendBid(int userID, int auctionID, int amount)
        {
            if (amount > 0)
            {
                User user = db.Users.FirstOrDefault(u => u.ID == userID);
                if (user == null)
                    return;

                Auction auction = db.Auctions.Find(auctionID);
                if (auction == null || auction.StatusID != 2 || auction.Deadline < DateTime.Now)
                    return;

                int highestBid = 0;
                if (auction.AuctionBids.Count > 0)
                    highestBid = auction.AuctionBids.Max(b => b.Amount);
                if (!(amount > highestBid && amount > auction.StartPrice))
                    return;

                try
                {
                    AuctionBid bid = new AuctionBid()
                    {
                        Amount = amount,
                        AuctionID = auctionID,
                        DateCreated = DateTime.Now,
                        UserID = userID
                    };
                    db.AuctionBids.Add(bid);

                    //auction.CurrentPrice += amount;
                    auction.CurrentPrice = amount;
                    db.SaveChanges();

                    ICurrencyService CurrencyService = DependencyResolver.Current.GetService<ICurrencyService>();
                    double dollarRate = CurrencyService.GetCurrencyRate();

                    string priceFormat = XCarsConfiguration.PriceFormat;

                    string PriceUSDStr = (auction.CurrencyID == 1) ? (auction.CurrentPrice.ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol) : (((int)(auction.CurrentPrice / dollarRate)).ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol);
                    string PriceUAHStr = (auction.CurrencyID == 2) ? (auction.CurrentPrice.ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol) : (((int)(auction.CurrentPrice * dollarRate)).ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol);

                    string name = user.FirstName + " " + user.LastName;
                    if (string.IsNullOrWhiteSpace(name))
                        name = Resource.NoName;
                    string city = user.City?.Name ?? Resource.NoCity;

                    List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == auctionID).ToList();
                    foreach (var item in currentAuctionConnections)
                    {
                        Clients.Client(item.Connection).broadcastBid(name, city, /*photoSrc,*/ amount.ToString(XCarsConfiguration.PriceFormat), bid.DateCreated.ToString("dd.MM.yyyy HH:mm"), PriceUSDStr, PriceUAHStr);
                    }
                }
                catch (Exception ex)
                { }
                //Clients.All.broadcastMessage();
            }
        }

        public void AddCurrentUserToAuction(string email, int auctionID)
        {
            try
            {
                //вход в аукцион пока бесплатный

                int? userID = null;

                User user = db.Users.FirstOrDefault(u => u.Email == email);
                //User user = _userService.GetCurrentUser();
                if (user != null)
                    userID = user.ID;

                AuctionConnection aucCnn = new AuctionConnection()
                {
                    AuctionID = auctionID,
                    UserID = userID,
                    Connection = Context.ConnectionId,
                    DateCreated = DateTime.Now
                };

                db.AuctionConnections.Add(aucCnn);
                db.SaveChanges();

                Auction auction = db.Auctions.FirstOrDefault(auc => auc.ID == auctionID);
                List<int> biddersIDs = auction.AuctionBids.Select(item => item.UserID).ToList();
                List<int> onlineAuthorizedUsersID = auction.AuctionConnections.Where(cnn => cnn.UserID != null).Select(item => (int)item.UserID).ToList();
                int onlineUnauthorizedUsersCount = auction.AuctionConnections.Where(cnn => cnn.UserID == null).Count();
                int NumberOfParticipants = biddersIDs.Union(onlineAuthorizedUsersID).Count() + onlineUnauthorizedUsersCount;

                List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == auctionID).ToList();
                foreach (var item in currentAuctionConnections)
                {
                    Clients.Client(item.Connection).participantsChanged(NumberOfParticipants);
                }



                //bool changeParticipantsCount = false;
                //if (user != null)
                //{
                //    if (db.AuctionBids.Where(bid => bid.AuctionID == auctionID && bid.UserID == user.ID).FirstOrDefault() == null
                //        && db.AuctionConnections.Where(cnn => cnn.AuctionID == auctionID && cnn.UserID == user.ID).Count() < 2)
                //    {
                //        changeParticipantsCount = true;
                //    }
                //}
                //else
                //    changeParticipantsCount = true;

                //if (changeParticipantsCount)
                //{
                //    List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == auctionID).ToList();
                //    foreach (var item in currentAuctionConnections)
                //    {
                //        Clients.Client(item.Connection).participantsChanged(1);
                //    }
                //}

                //_auctionConnectionService.Create(aucCnn);
            }
            catch (Exception ex)
            {
            }
        }

        public void FinishAuction(int userID, int auctionID)
        {
            Auction auction = db.Auctions.Find(auctionID);
            if (auction == null || auction.Auto.UserID != userID || auction.StatusID != 2)
                return;

            try
            {
                //auction.StateID = 3;
                //db.SaveChanges();

                List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == auctionID).ToList();
                foreach (var item in currentAuctionConnections)
                {
                    Clients.Client(item.Connection).auctionFinishes(auctionID);
                }
            }
            catch (Exception ex)
            { }
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //string name = Context.User.Identity.Name;
            //making user online
            int k = 0;
            k++;

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            //removing user connection
            //_auctionConnectionService.Delete(Context.ConnectionId);
            AuctionConnection cnn = db.AuctionConnections.FirstOrDefault(c => c.Connection == Context.ConnectionId);
            if (cnn != null)
            {
                try
                {
                    //bool changeParticipantsCount = false;
                    //if (cnn.User != null)
                    //{
                    //    if (db.AuctionBids.Where(bid => bid.AuctionID == cnn.AuctionID && bid.UserID == cnn.UserID).FirstOrDefault() == null
                    //        && db.AuctionConnections.Where(con => con.AuctionID == cnn.AuctionID && con.UserID == cnn.UserID).Count() == 1)
                    //    {
                    //        changeParticipantsCount = true;
                    //    }
                    //}
                    //else
                    //    changeParticipantsCount = true;

                    //if (changeParticipantsCount)
                    //{
                    //    List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == cnn.AuctionID).ToList();
                    //    foreach (var item in currentAuctionConnections)
                    //    {
                    //        Clients.Client(item.Connection).participantsChanged(-1);
                    //    }
                    //}
                    Auction auction = db.Auctions.FirstOrDefault(auc => auc.ID == cnn.AuctionID);

                    db.AuctionConnections.Remove(cnn);
                    db.SaveChanges();

                    
                    if (auction != null)
                    {
                        List<int> biddersIDs = auction.AuctionBids.Select(item => item.UserID).ToList();
                        List<int> onlineAuthorizedUsersID = auction.AuctionConnections.Where(con => con.UserID != null).Select(item => (int)item.UserID).ToList();
                        int onlineUnauthorizedUsersCount = auction.AuctionConnections.Where(con => con.UserID == null).Count();
                        int NumberOfParticipants = biddersIDs.Union(onlineAuthorizedUsersID).Count() + onlineUnauthorizedUsersCount;

                        List<AuctionConnection> currentAuctionConnections = db.AuctionConnections.Where(c => c.AuctionID == auction.ID).ToList();
                        foreach (var item in currentAuctionConnections)
                        {
                            Clients.Client(item.Connection).participantsChanged(NumberOfParticipants);
                        }
                    }
                }
                catch (Exception ex)
                { }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}