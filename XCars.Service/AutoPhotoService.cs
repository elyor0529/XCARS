using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;
using XCars.Common;

namespace XCars.Service
{
    public class AutoPhotoService : BaseService<AutoPhoto>, IAutoPhotoService
    {
        public IFileManager FileManager { get; set; }
        public IAutoIndexService AutoIndexService { get; set; }

        public AutoPhotoService(IAutoPhotoRepository autoPhotoRepository, IUnitOfWork unitOfWork)
            : base(autoPhotoRepository, unitOfWork)
        {
        }

        public int UploadPhoto(int autoID, HttpPostedFileBase photo)
        {
            int photoID = 0;
            if (photo != null)
            {
                try
                {
                    //string path = "autophotos/tmp/";
                    //string filename = "asdf";
                    AutoPhoto autoPhoto = new AutoPhoto()
                    {
                        AutoID = autoID,
                        //Url = "asdf",
                        IsMain = false
                    };

                    Create(autoPhoto);
                    photoID = autoPhoto.ID;

                    if (autoPhoto.Auto.AutoPhotoes.Count == 1)
                        autoPhoto.IsMain = true;

                    //filename = autoID + "_" + autoPhoto.ID + ".jpg";
                    //autoPhoto.Url = "autophotos/thumbnail/" + filename;

                    string filename = photoID + XCarsConfiguration.PhotoExtension;

                    if (FileManager.SaveFile(photo, XCarsConfiguration.AutoPhotosTempUrl, filename))
                    {
                        Edit(autoPhoto);
                        AutoIndexService.UpdateIndex(autoPhoto.Auto);
                    }
                    else
                        Delete(autoPhoto.ID);
                }
                catch (Exception ex)
                { }
            }

            return photoID;
        }

        public void Create(AutoPhoto photo)
        {
            this._repository.Add(photo);
            Save();
        }

        public void Edit(AutoPhoto photo)
        {
            this._repository.Update(photo);
            Save();
        }

        public void EditMany(IEnumerable<AutoPhoto> autos)
        {
            foreach (var item in autos)
            {
                this._repository.Update(item);
            }
            Save();
        }

        public void MakePhotoMain(int id)
        {
            AutoPhoto photo = this._repository.GetById(id);
            if (photo != null)
            {
                List<AutoPhoto> photos = photo.Auto.AutoPhotoes.ToList();
                for (int i = 0; i < photos.Count; i++)
                    photos[i].IsMain = false;

                EditMany(photos);
            }

            photo.IsMain = true;
            Edit(photo);

            AutoIndexService.UpdateIndex(photo.Auto);
        }

        public int Delete(int id)
        {
            int mainPhotoID = 0;
            AutoPhoto photo = this._repository.GetById(id);
            if (photo != null)
            {
                if (photo.IsMain && photo.Auto.AutoPhotoes.Count > 1)
                {
                    AutoPhoto otherPhoto = photo.Auto.AutoPhotoes.FirstOrDefault(p => !p.IsMain && p.ID != id);
                    if (otherPhoto != null)
                    {
                        otherPhoto.IsMain = true;
                        Edit(otherPhoto);
                        mainPhotoID = otherPhoto.ID;
                    }

                    photo.IsMain = false;
                    Edit(photo);
                }

                //FileManager.DeleteFile(photo.Url);
                //int autoID = photo.AutoID;
                Auto auto = photo.Auto;

                this._repository.Delete(photo);
                Save();

                AutoIndexService.UpdateIndex(auto);
            }

            return mainPhotoID;
        }
    }
}
