using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Model;
using System.ComponentModel.DataAnnotations;
using XCars.Resourses;
using XCars.Common;
using XCars.Service.Interfaces;
using XCars.Service;

namespace XCars.ViewModels
{
    public class CityVM
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static implicit operator CityVM(City model)
        {
            return new CityVM
            {
                ID = model?.ID ?? 0,
                Name = model?.Name
            };
        }
    }

    public class PersonalDataVM
    {
        public int ID { get; set; }

        //[Required]
        //[Display(Name = "UserFullName", ResourceType = typeof(Resource))]
        //public string Name { get; set; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Region", ResourceType = typeof(Resource))]
        public int RegionID { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public int CityID { get; set; }

        [Required]
        [Display(Name = "Telephone", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public string PhotoSrc { get; set; }

        public static implicit operator PersonalDataVM(User model)
        {
            //string[] tmp = model.PhotoUrl.Split('/');
            //tmp = tmp[tmp.Length - 1].Split('.');

            //string photoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + XCarsConfiguration.UserPhotosUploadUrl;
            //if (tmp[0] == model.ID.ToString())
            //    photoSrc += model.ID + "_180x180" + XCarsConfiguration.PhotoExtension;
            //else
            //    photoSrc += XCarsConfiguration.UserNoPhotoName + XCarsConfiguration.PhotoExtension;

            string photoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + model.PhotoUrl;

            return new PersonalDataVM
            {
                ID = model?.ID ?? 0,
                CityID = model?.CityID ?? 0,
                //Name = model?.FirstName + " " + model?.LastName,
                FirstName = model?.FirstName,
                LastName = model?.LastName,
                PhoneNumber = model?.PhoneNumber,
                RegionID = model?.RegionID ?? 0,
                //PhotoSrc = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(fileManager.GetFile(model?.PhotoUrl ?? $"{XCarsConfiguration.UserNoPhotoUrl}"))
                //PhotoSrc = model?.PhotoUrl == null ? $"/{XCarsConfiguration.UserNoPhotoUrl}" : $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + model.PhotoUrl 
                //PhotoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + (model.PhotoUrl ?? $"{XCarsConfiguration.UserNoPhotoUrl}")
                PhotoSrc = photoSrc
            };
        }

        //public static implicit operator PersonalData(PersonalDataVM vm)
        //{
        //    return new PersonalData
        //    {
        //        CityID = vm.CityID,
        //        Name = vm.Name
        //    };
        //}
    }

    public class UserShortVM
    {
        public int UserID { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public bool IsOnline { get; set; }

        [Display(Name = "LastSeen", ResourceType = typeof(Resource))]
        public DateTime? LastSeen { get; set; }

        [Display(Name = "UserFullName", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Resource))]
        public string Region { get; set; }

        [Display(Name = "DateRegistered", ResourceType = typeof(Resource))]
        public string DateRegistered { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = "Contacts", ResourceType = typeof(Resource))]
        public string Contacts { get; set; }
        public bool ContactsIsHiddenByDefault { get; set; } = false;

        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public string PhotoSrc { get; set; }

        public int Balance { get; set; }

        public static implicit operator UserShortVM(User model)
        {
            //string[] tmp = model.PhotoUrl.Split('/');
            //tmp = tmp[tmp.Length - 1].Split('.');

            //string photoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + XCarsConfiguration.UserPhotosUploadUrl;
            //if (tmp[0] == model.ID.ToString())
            //    photoSrc += model.ID + "_180x180" + XCarsConfiguration.PhotoExtension;
            //else
            //    photoSrc += XCarsConfiguration.UserNoPhotoName + XCarsConfiguration.PhotoExtension;

            string photoSrc = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + model.PhotoUrl;

            return new UserShortVM
            {
                UserID = model.ID,
                //Name = model.Name,
                FirstName = model?.FirstName,
                LastName = model?.LastName,
                Name = model?.FirstName + " " + model?.LastName,
                Region = !(model.Region == null && model.City == null) ? model.Region?.Name + ", " + model.City?.Name : null,
                DateRegistered = model.DateRegistered.ToString("dd.MM.yy"),
                Contacts = model.PhoneNumber,
                ContactsIsHiddenByDefault = true,
                Email = model.Email,
                //PhotoSrc = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(fileManager.GetFile(model.PersonalData?.PhotoUrl ?? $"{XCarsConfiguration.UserNoPhotoUrl}")),
                PhotoSrc = photoSrc,
                //PhotoSrc = "http://www.kino-teatr.ru/art/4266/56593.jpg",
                IsOnline = model.UserConnections.Count > 0 ? true : false,
                LastSeen = (model.UserConnections.Count == 0) ? (model.LastSeen ?? null) : null,
                Balance = (int)model.Balance
            };
        }
    }
}