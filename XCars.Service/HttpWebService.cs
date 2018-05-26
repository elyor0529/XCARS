using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class HttpWebService : IHttpWebService
    {
        public HttpWebResponse MakeHttpWebRequest(string url, string method, string postData = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            if (!string.IsNullOrWhiteSpace(postData))
            {
                var data = Encoding.UTF8.GetBytes(postData);
                
                request.Accept = "application/json";
                if (method.ToLower() != "get")
                    request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
