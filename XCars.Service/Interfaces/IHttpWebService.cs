using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XCars.Service.Interfaces
{
    public interface IHttpWebService
    {
        HttpWebResponse MakeHttpWebRequest(string url, string method, string postData = null);
    }
}
