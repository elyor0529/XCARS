using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Common;

namespace Amazon.AwsS3
{
    public class AmazonS3
    {
        static string bucketName = $"{XCarsConfiguration.BucketName}";
        static IAmazonS3 client;

        public AmazonS3()
        {
            //client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
        }

        public static byte[] GetFile(string path, string fileName) //path = "userphoto/", fileName = "photo.jpg"
        {
            return new byte[0];
            try
            {
                client = new AmazonS3Client();
                GetObjectRequest req = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = path + fileName
                };

                GetObjectResponse res = client.GetObject(req);

                using (MemoryStream ms = new MemoryStream())
                {
                    res.ResponseStream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }

        public static void UploadFile(Stream inputStream, string path, string filename) //path = "userphotos/tmp/", filename = "photo.jpg"
        {
            string body = "success";
            try
            {
                //client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);
                //client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);
                client = new AmazonS3Client();
                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    InputStream = inputStream,
                    Key = path + filename,
                    CannedACL = S3CannedACL.Private
                };
                PutObjectResponse response2 = client.PutObject(request);
            }
            catch (Exception ex)
            {
                body = "Error: " + ex.Message;
            }

            //EmailServiceReference.SMTPServiceClient emailServiceClient = new EmailServiceReference.SMTPServiceClient();
            //emailServiceClient.Send(null, "cortex91@inbox.ru", "Upload File", body);
        }

        public static void DeleteFile(string path, string fileName) //path = "userphotos/", fileName = "photo.jpg"
        {
            try
            {
                client = new AmazonS3Client();
                DeleteObjectRequest req = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = path + fileName
                };

                DeleteObjectResponse res = client.DeleteObject(req);
            }
            catch
            { }
        }
    }
}
