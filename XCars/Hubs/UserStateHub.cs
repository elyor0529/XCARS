using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using XCars.Model;
using System.Threading;
using XCars.Service.Interfaces;
using System.Web.Mvc;

namespace XCars.Hubs
{
    public class UserStateHub : Hub
    {
        XCarsEntities db = new XCarsEntities();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void AddConnectionIDforCurrentUser(string email)
        {
            try
            {
                //IUserService userService = DependencyResolver.Current.GetService<IUserService>();
                
                
                User user = db.Users.FirstOrDefault(u => u.Email == email);
                //User user1 = userService.GetUserByEmail(email);
                if (user != null)
                {
                    UserConnection connection = db.UserConnections.FirstOrDefault(c => c.UserID == user.ID && c.Connection == Context.ConnectionId);
                    if (connection == null)
                    {
                        user.UserConnections.Add(new UserConnection()
                        {
                            Connection = Context.ConnectionId,
                            DateCreated = DateTime.Now
                        });
                    }
                    else
                        connection.DateCreated = DateTime.Now;

                    db.SaveChanges();

                    foreach (var item in db.UserConnections.Where(c => c.UserID != user.ID))
                    {
                        Clients.Client(item.Connection).userIsOnline(user.ID);
                    }
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

        public override System.Threading.Tasks.Task OnReconnected()
        {
            Clients.Caller.reconnect();
            return base.OnReconnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            //removing user connection
            try
            {
                UserConnection connection = db.UserConnections.FirstOrDefault(c => c.Connection == Context.ConnectionId);
                if (connection != null)
                {
                    Thread.Sleep(5000);
                    int userID = connection.UserID;
                    db.UserConnections.Remove(connection);
                    db.SaveChanges();

                    

                    User user = db.Users.Find(userID);
                    if (user != null)
                    {
                        if (user.UserConnections.Count == 0)
                        {
                            foreach (var item in db.UserConnections)
                            {
                                Clients.Client(item.Connection).userIsOffline(user.ID);
                            }

                            user.LastSeen = DateTime.Now;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return base.OnDisconnected(stopCalled);
        }
    }
}