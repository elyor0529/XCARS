using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using XCars.Common;

namespace XCars.Service
{
    public interface IEmailService
    {
        void Send(string fromAddress, string toAddress, string messageSubject, string body);
    }
    public class SMTPEmailService : IEmailService
    {
        //Create an SMTPClient that will handle sending your email
        private static SmtpClient client = new SmtpClient($"{XCarsConfiguration.SMTPhost}", Int32.Parse($"{XCarsConfiguration.SMTPhostPort}"));

        public SMTPEmailService()
        {
            client.UseDefaultCredentials = false;
            //Set up your Credentials of where to send it from
            client.Credentials = new NetworkCredential($"{XCarsConfiguration.SMTPhostUsername}", $"{XCarsConfiguration.SMTPhostPassword}");
        }

        public void Send(string fromAddress, string toAddress, string messageSubject, string body)
        {
            fromAddress = $"{XCarsConfiguration.SMTPhostFromAddress}";

            MailAddress from = new MailAddress(fromAddress, $"{XCarsConfiguration.SMTPhostFromDisplayName}");
            MailAddress to = new MailAddress(toAddress, toAddress);

            //Build your actual message to send
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = messageSubject;
            mm.Body = body;
            mm.IsBodyHtml = true;
            mm.Sender = from;
            mm.Headers.Add("Reply-To", $"{XCarsConfiguration.SMTPhostFromAddress}");

            //Set up your encoding
            mm.BodyEncoding = UTF8Encoding.UTF8;

            //Send your message
            client.Send(mm);
        }
    }
}
