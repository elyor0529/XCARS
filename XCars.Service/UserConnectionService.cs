using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class UserConnectionService : BaseService<UserConnection>, IUserConnectionService
    {
        public IUserService UserService { get; set; }

        public UserConnectionService(IUserConnectionRepository userConnectionRepository,
                                        IUnitOfWork unitOfWork)
            : base(userConnectionRepository, unitOfWork)
        {
        }

        public void Create(UserConnection model)
        {
            this._repository.Add(model);
            Save();
        }

        public UserConnection GetByCnnID(string connectionID)
        {
            return this._repository.Get(a => a.Connection == connectionID);
        }

        public void Delete(string connectionID)
        {
            UserConnection connection = GetByCnnID(connectionID);
            if (connection != null)
            {
                this._repository.Delete(connection);
                Save();
            }
        }

        private void Delete(UserConnection connection)
        {
            this._repository.Delete(connection);
            Save();
        }

        public void ClearAll()
        {
            List<User> users = UserService.GetAll().Where(c => c.UserConnections.FirstOrDefault() != null).ToList();
            for (int j = 0; j < users.Count; j++)
            {
                List<UserConnection> connections = users[j].UserConnections.OrderByDescending(c => c.DateCreated).ToList();
                for (int i = 0; i < connections.Count; i++)
                {
                    if (i == 0)
                        users[j].LastSeen = connections[i].DateCreated;
                    Delete(connections[i]);
                }

                UserService.EditUser(users[j]);
            }
        }
    }
}