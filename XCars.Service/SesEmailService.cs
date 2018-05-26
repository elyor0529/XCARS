using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Amazon.AwsS3;
using XCars.Common;

namespace XCars.Service
{
    //public interface IEmailService
    //{
    //    void Send(string fromAddress, string toAddress, string messageSubject, string body);
    //}
    public class SesEmailService : IEmailService
    {
        public SesEmailService()
        {

        }

        public void Send(string fromAddress, string toAddress, string messageSubject, string body)
        {
            var amazonSes = new AmazonSES();
            amazonSes.Send(messageSubject, new List<string>() {toAddress}, fromAddress ,"", body);
        }
    }
}
