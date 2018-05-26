using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using XCars.Common;

namespace Amazon.AwsS3
{
    public class AmazonSES
    {
        static readonly string configSet = "";

        public AmazonSES()
        {

        }

        public void Send(string subject, List<string> receiversAddresses, string senderAddress, string textBody, string htmlBody)
        {
            //string AwsAccessKey = $"{XCarsConfiguration.AWSAccessKey}";
            //string AwsSecretKey = $"{XCarsConfiguration.AWSSecretKey}";

            senderAddress = string.IsNullOrEmpty(senderAddress) ? $"{XCarsConfiguration.AWSFromAddress}" : senderAddress;

            //using (var client = new AmazonSimpleEmailServiceClient(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            using (var client = new AmazonSimpleEmailServiceClient())
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = senderAddress,
                    Destination = new Destination
                    {
                        ToAddresses = receiversAddresses
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = htmlBody
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = textBody
                            }
                        }
                    },
                    ConfigurationSetName = configSet
                };
                try
                {
                    //Sending email using Amazon SES...
                    var response = client.SendEmail(sendRequest);
                    //The email was sent successfully.
                }
                catch (Exception ex)
                {
                    //The email was not sent.
                    //Error message: " + ex.Message
                    throw ex;
                }
            }
        }
    }
}
