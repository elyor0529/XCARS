using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public IFileManager FileManager { get; set; }

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
            : base(userRepository, unitOfWork)
        {
        }

        public void CreateUser(User user)
        {
            this._repository.Add(user);
            Save();
        }

        public void CreateUserFromAspNetUser(dynamic aspNetUser, string phoneNumber = null, string firstName = null, string lastName = null,/* string userFullName = null,*/ byte[] profileImageBytes = null)
        {
            User user = new User()
            {
                AspNetUserID = aspNetUser.Id,
                //Name = userFullName,
                FirstName = firstName,
                LastName = lastName,
                Email = aspNetUser.Email,
                DateRegistered = DateTime.Now,
                Balance = 0.0M,
                AuctionAgreement = false,
                PhoneNumber = phoneNumber,
                PhotoUrl = XCarsConfiguration.UserPhotosUploadUrl + XCarsConfiguration.UserNoPhotoName + XCarsConfiguration.PhotoExtension
            };

            CreateUser(user);
            if (profileImageBytes != null)
                user.PhotoUrl = CreateUserPhotoFromFBProfileImageBytesAndGetUrl(user, profileImageBytes);
            EditUser(user);
        }

        public void EditUser(User user)
        {
            this._repository.Update(user);
            Save();
        }

        private string CreateUserPhotoFromFBProfileImageBytesAndGetUrl(User user, byte[] profileImageBytes = null)
        {
            string filename = user.ID + XCarsConfiguration.PhotoExtension;
            string path = XCarsConfiguration.UserPhotosTempUrl;

            Stream stream = new MemoryStream(profileImageBytes);
            FileManager.SaveFileFromStream(stream, path, filename);

            //лямбда не успевает создать thumbnail. поэтому добавил эту задержку
            Thread.Sleep(2000);

            return XCarsConfiguration.UserPhotosUploadUrl + user.ID + "_180x180" + XCarsConfiguration.PhotoExtension;
        }

        public string SaveUserPhotoAndGetUrl(User user, HttpPostedFileBase file)
        {
            string filename = user.ID + XCarsConfiguration.PhotoExtension;
            string path = XCarsConfiguration.UserPhotosTempUrl;

            FileManager.SaveFile(file, path, filename);

            return XCarsConfiguration.UserPhotosUploadUrl + user.ID + "_180x180" + XCarsConfiguration.PhotoExtension;
        }

        public void DeleteUserPhotoFile(User user)
        {
            FileManager.DeleteFile(XCarsConfiguration.UserPhotosUploadUrl + user.ID + XCarsConfiguration.PhotoExtension);
            user.PhotoUrl = XCarsConfiguration.UserPhotosUploadUrl + XCarsConfiguration.UserNoPhotoName + XCarsConfiguration.PhotoExtension;
            EditUser(user);
        }

        public User GetUser(string userId)
        {
            return this._repository.GetById(userId);
        }

        public User GetUser(int userId)
        {
            return this._repository.GetById(userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return this._repository.GetAll();
        }

        public IQueryable<User> GetUsersAny()
        {
            return this._repository.GetAny();
        }

        public User GetUserByEmail(string email)
        {
            var user = this._repository.Get(u => u.Email.Contains(email));
            return user;
        }

        //public IEnumerable<User> GetUsersByPage(int currentPage, int noOfRecords, string sortBy, string filterBy)
        //{
        //    return userRepository.GetUsersByPage(currentPage, noOfRecords, sortBy, filterBy);
        //}

        //public void AttachCollection(ICollection<ApplicationUser> users)
        //{
        //    userRepository.AttachCollection(users);
        //}

        public User GetCurrentUser()
        {
            if (HttpContext.Current == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            User user = GetUserByEmail(HttpContext.Current.User.Identity.Name);
            return user;
        }
    }
}
