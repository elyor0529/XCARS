using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> GetUsers();
        //IEnumerable<User> GetUsersByPage(int currentPage, int noOfRecords, string sortBy, string filterBy);
        User GetUser(string userId);
        IQueryable<User> GetUsersAny();
        User GetUserByEmail(string email);
        void CreateUser(User user);
        void CreateUserFromAspNetUser(dynamic aspNetUser, string phoneNumber = null, string firstName = null, string lastName = null,/* string userFullName = null,*/ byte[] profileImageBytes = null);
        void EditUser(User user);
        string SaveUserPhotoAndGetUrl(User user, HttpPostedFileBase file);
        void DeleteUserPhotoFile(User user);
        void Save();
        //void AttachCollection(ICollection<User> users);
        User GetCurrentUser();
    }
}
