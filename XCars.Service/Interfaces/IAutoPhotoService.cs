using System.Collections.Generic;
using System.Web;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoPhotoService : IBaseService<AutoPhoto>
    {
        void Create(AutoPhoto photo);
        void Edit(AutoPhoto photo);
        void EditMany(IEnumerable<AutoPhoto> photos);
        int UploadPhoto(int autoID, HttpPostedFileBase photo);
        //List<AutoPhoto> UploadPhotos(int autoID, IEnumerable<HttpPostedFileBase> photos);
        void MakePhotoMain(int id);
        int Delete(int id);
    }
}
