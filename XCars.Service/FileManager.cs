using Amazon.AwsS3;
using System;
using System.IO;
using System.Web;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class FileManager : IFileManager
    {
        public bool SaveFile(HttpPostedFileBase file, string path, string filename)
        {
            bool result = false;

            if (file != null)
            {
                try
                {
                    //file.SaveAs(HttpContext.Current.Server.MapPath("~" + path) + filename);
                    AmazonS3.UploadFile(file.InputStream, path, filename);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public bool SaveFileFromStream(Stream inputStream, string path, string filename)
        {
            bool result = false;

            if (inputStream != null)
            {
                try
                {
                    AmazonS3.UploadFile(inputStream, path, filename);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public byte[] GetFile(string fileName) // fileName = "/path/file.ext"
        {
            string[] tmp = fileName.Split('/');
            string path = "";
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                if (tmp[i] != "")
                    path += tmp[i] + "/";
            }
            fileName = tmp[tmp.Length - 1];

            return AmazonS3.GetFile(path, fileName);
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~" + url)))
                //    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~" + url));
                string[] tmp = fileName.Split('/');
                string path = "";
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    if (tmp[i] != "")
                        path += tmp[i] + "/";
                }
                fileName = tmp[tmp.Length - 1];

                AmazonS3.DeleteFile(path, fileName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
