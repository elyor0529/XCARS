using System.Collections.Generic;
using System.Web;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAuctionPhotoService : IBaseService<AuctionPhoto>
    {
        void Create(AuctionPhoto photo);
        void Edit(AuctionPhoto photo);
        void EditMany(IEnumerable<AuctionPhoto> photos);
        int UploadPhoto(int auctionID, HttpPostedFileBase photo);
        //void UploadPhotos(int auctionID, IEnumerable<HttpPostedFileBase> photos);
        void MakePhotoMain(int id);
        int Delete(int id);
    }
}