using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AuctionPhotoService : BaseService<AuctionPhoto>, IAuctionPhotoService
    {
        public IFileManager FileManager { get; set; }

        //should be uncommented once indexing for auctions is implemented
        //public IAuctionIndexService AuctionIndexService { get; set; }

        public AuctionPhotoService(IAuctionPhotoRepository auctionPhotoRepository, IUnitOfWork unitOfWork)
            : base(auctionPhotoRepository, unitOfWork)
        {
        }

        public int UploadPhoto(int auctionID, HttpPostedFileBase photo)
        {
            int photoID = 0;
            if (photo != null)
            {
                try
                {
                    AuctionPhoto auctionPhoto = new AuctionPhoto()
                    {
                        AuctionID = auctionID,
                        IsMain = false
                    };

                    Create(auctionPhoto);
                    photoID = auctionPhoto.ID;

                    if (auctionPhoto.Auction.AuctionPhotoes.Count == 1)
                        auctionPhoto.IsMain = true;

                    string filename = photoID + XCarsConfiguration.PhotoExtension;

                    if (FileManager.SaveFile(photo, XCarsConfiguration.AuctionPhotosTempUrl, filename))
                        Edit(auctionPhoto);
                    else
                        Delete(auctionPhoto.ID);

                    //should be uncommented once indexing for auctions is implemented
                    //AuctionIndexService.UpdateIndex(auctionPhoto.Auction);
                }
                catch (Exception ex)
                { }
            }

            return photoID;
        }

        public void Create(AuctionPhoto photo)
        {
            this._repository.Add(photo);
            Save();
        }

        public void Edit(AuctionPhoto photo)
        {
            this._repository.Update(photo);
            Save();
        }

        public void EditMany(IEnumerable<AuctionPhoto> autos)
        {
            foreach (var item in autos)
            {
                this._repository.Update(item);
            }
            Save();
        }

        public void MakePhotoMain(int id)
        {
            AuctionPhoto photo = this._repository.GetById(id);
            if (photo != null)
            {
                List<AuctionPhoto> photos = photo.Auction.AuctionPhotoes.ToList();
                for (int i = 0; i < photos.Count; i++)
                    photos[i].IsMain = false;

                EditMany(photos);
            }

            photo.IsMain = true;
            Edit(photo);

            //should be uncommented once indexing for auctions is implemented
            //AuctionIndexService.UpdateIndex(photo.Auction);
        }

        public int Delete(int id)
        {
            int mainPhotoID = 0;
            AuctionPhoto photo = this._repository.GetById(id);
            if (photo != null)
            {
                if (photo.IsMain && photo.Auction.AuctionPhotoes.Count > 1)
                {
                    AuctionPhoto otherPhoto = photo.Auction.AuctionPhotoes.FirstOrDefault(p => !p.IsMain && p.ID != id);
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
                //int auctionID = photo.AuctionID;
                Auction auction = photo.Auction;

                this._repository.Delete(photo);
                Save();

                //should be uncommented once indexing for auctions is implemented
                //AuctionIndexService.UpdateIndex(auction);
            }

            return mainPhotoID;
        }
    }
}
