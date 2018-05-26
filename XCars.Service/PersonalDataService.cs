using System.Web;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    //public class PersonalDataService : BaseService<PersonalData>, IPersonalDataService
    //{
    //    public IFileManager FileManager { get; set; }

    //    public PersonalDataService(IPersonalDataRepository userRepository, IUnitOfWork unitOfWork)
    //        : base(userRepository, unitOfWork)
    //    {
    //    }

    //    public void CreatePD(PersonalData pd)
    //    {
    //        this._repository.Add(pd);
    //        Save();
    //    }

    //    public void EditPD(PersonalData pd)
    //    {
    //        this._repository.Update(pd);
    //        Save();
    //    }

    //    public string SaveUserPhotoAndGetUrl(User user, HttpPostedFileBase file)
    //    {
    //        string filename = user.ID + ".jpg";
    //        string path = "userphotos/tmp/";

    //        FileManager.SaveFile(file, path, filename);

    //        return "userphotos/thumbnail/" + filename;
    //    }

    //    public void DeleteUserPhotoFile(User user)
    //    {
    //        PersonalData model = this._repository.GetById((int)user.PersonalDataID);
    //        model.PhotoUrl = $"{XCarsConfiguration.UserNoPhotoUrl}";
    //        EditPD(model);
    //        FileManager.DeleteFile("userphotos/origin/" + user.ID + ".jpg");
    //    }
    //}
}
