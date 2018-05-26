using System.IO;
using System.Web;

namespace XCars.Service.Interfaces
{
    public interface IFileManager
    {
        bool SaveFile(HttpPostedFileBase file, string path, string filename);
        bool SaveFileFromStream(Stream inputStream, string path, string filename);
        bool DeleteFile(string fileName);
        byte[] GetFile(string fileName);
    }
}
