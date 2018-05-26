using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCars.Common
{
    public static class XCarsConfiguration
    {
        public static string AmazonESDomenEndpoint
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenEndpoint");
            }
        }
        public static string AmazonESDomenIndex
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenIndex");
            }
        }
        public static string AmazonESDomenIndexMapping
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenIndexMapping");
            }
        }
        public static string AmazonESDomenIndexAuction
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenIndexAuction");
            }
        }
        public static string AmazonESDomenIndexMappingAuction
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenIndexMappingAuction");
            }
        }

        public static string AmazonESDomenIndexBulkUpdateMaxLength
        {
            get
            {
                return ConfigurationHelper.GetSetting("AmazonESDomenIndexBulkUpdateMaxLength");
            }
        }
        public static string ElasticSearchDateFormat
        {
            get
            {
                return ConfigurationHelper.GetSetting("ElasticSearchDateFormat");
            }
        }

        //SMTP
        public static string SMTPhost
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhost");
            }
        }
        public static string SMTPhostPort
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhostPort");
            }
        }
        public static string SMTPhostUsername
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhostUsername");
            }
        }
        public static string SMTPhostPassword
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhostPassword");
            }
        }
        public static string SMTPhostFromAddress
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhostFromAddress");
            }
        }
        public static string SMTPhostFromDisplayName
        {
            get
            {
                return ConfigurationHelper.GetSetting("SMTPhostFromDisplayName");
            }
        }

        public static string AllowUnauthenticatedUserToEnterAuction
        {
            get
            {
                return ConfigurationHelper.GetSetting("AllowUnauthenticatedUserToEnterAuction");
            }
        }
        public static string AuctionEnterPrice
        {
            get
            {
                return ConfigurationHelper.GetSetting("AuctionEnterPrice");
            }
        }
        public static string XMinutesRemaingToAuctionDeadline
        {
            get
            {
                return ConfigurationHelper.GetSetting("XMinutesRemaingToAuctionDeadline");
            }
        }
        public static string XHoursRemaingToDraftAuctionDeletion
        {
            get
            {
                return ConfigurationHelper.GetSetting("XHoursRemaingToDraftAuctionDeletion");
            }
        }
        public static string CurrencyCheckIntervalMinutes
        {
            get
            {
                return ConfigurationHelper.GetSetting("CurrencyCheckIntervalMinutes");
            }
        }
        public static string EmailCheckIntervalMinutes
        {
            get
            {
                return ConfigurationHelper.GetSetting("EmailCheckIntervalMinutes");
            }
        }
        public static string AutoNoPhotoUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoNoPhotoUrl");
            }
        }
        public static string AutoNoPhotoName
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoNoPhotoName");
            }
        }
        public static string ImageSourceType
        {
            get
            {
                return ConfigurationHelper.GetSetting("ImageSourceType");
            }
        }
        public static string UserNoPhotoUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("UserNoPhotoUrl");
            }
        }
        public static string UserNoPhotoName
        {
            get
            {
                return ConfigurationHelper.GetSetting("UserNoPhotoName");
            }
        }
        public static string ClearGif
        {
            get
            {
                return ConfigurationHelper.GetSetting("ClearGif");
            }
        }

        //Amazon
        public static string BucketEndpoint
        {
            get
            {
                return ConfigurationHelper.GetSetting("BucketEndpoint");
            }
        }
        public static string BucketName
        {
            get
            {
                return ConfigurationHelper.GetSetting("BucketName");
            }
        }
        public static string BucketAccessKeyID
        {
            get
            {
                return ConfigurationHelper.GetSetting("BucketAccessKeyID");
            }
        }
        public static string BucketSecretAccessKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("BucketSecretAccessKey");
            }
        }
        public static string UserPhotosTempUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("UserPhotosTempUrl");
            }
        }
        public static string UserPhotosUploadUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("UserPhotosUploadUrl");
            }
        }
        public static string AutoPhotosTempUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPhotosTempUrl");
            }
        }
        public static string AutoPhotosUploadUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPhotosUploadUrl");
            }
        }
        public static string AuctionPhotosTempUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AuctionPhotosTempUrl");
            }
        }
        public static string AuctionPhotosUploadUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AuctionPhotosUploadUrl");
            }
        }

        public static string MinfinComUaUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("MinfinComUaUrl");
            }
        }
        public static string MinfinComUaAccessKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("MinfinComUaAccessKey");
            }
        }

        public static string XMinutesAuctionFinishEmailEligiblePeriod
        {
            get
            {
                return ConfigurationHelper.GetSetting("XMinutesAuctionFinishEmailEligiblePeriod");
            }
        }
        public static string XMinutesRemainingAuctionFinishEmailEligiblePeriod
        {
            get
            {
                return ConfigurationHelper.GetSetting("XMinutesRemainingAuctionFinishEmailEligiblePeriod");
            }
        }
        public static string XMinutesAutoFinishEmailEligiblePeriod
        {
            get
            {
                return ConfigurationHelper.GetSetting("XMinutesAutoFinishEmailEligiblePeriod");
            }
        }

        public static string RecommendedPriceRates
        {
            get
            {
                return ConfigurationHelper.GetSetting("RecommendedPriceRates");
            }
        }

        public static string AutoPublishTopMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishTopMin");
            }
        }
        public static string AutoPublishTopMax
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishTopMax");
            }
        }
        public static string AutoPublishDaysMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishDaysMin");
            }
        }
        public static string AutoPublishDaysMax
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishDaysMax");
            }
        }
        public static string AutoPublishOncePayedCost
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishOncePayedCost");
            }
        }
        public static string AutoPublishTopDefault
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishTopDefault");
            }
        }
        public static string AutoPublishDaysDefault
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishDaysDefault");
            }
        }

        public static string AutoEngineCapacityMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoEngineCapacityMin");
            }
        }
        public static string AutoEngineCapacityMax
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoEngineCapacityMax");
            }
        }
        public static string AutoEngineCapacityStep
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoEngineCapacityStep");
            }
        }

        public static string AutoFuelConsumptionMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoFuelConsumptionMin");
            }
        }
        public static string AutoFuelConsumptionMax
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoFuelConsumptionMax");
            }
        }
        public static string AutoFuelConsumptionStep
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoFuelConsumptionStep");
            }
        }

        public static string YearMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("YearMin");
            }
        }

        public static string AutoPowerMin
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPowerMin");
            }
        }
        public static string AutoPowerMax
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPowerMax");
            }
        }
        public static string AutoPowerStep
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPowerStep");
            }
        }

        public static string LMI_MERCHANT_ID
        {
            get
            {
                return ConfigurationHelper.GetSetting("LMI_MERCHANT_ID");
            }
        }
        public static string LMI_MODE
        {
            get
            {
                return ConfigurationHelper.GetSetting("LMI_MODE");
            }
        }
        public static string AutoPublishSuccessUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishSuccessUrl");
            }
        }
        public static string LMI_secretKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("LMI_secretKey");
            }
        }

        public static string AutoPublishPaymentFailUrl
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoPublishPaymentFailUrl");
            }
        }

        public static string PriceFormat
        {
            get
            {
                return ConfigurationHelper.GetSetting("PriceFormat");
            }
        }

        public static string AWSAccessKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("AWSAccessKey");
            }
        }
        public static string AWSSecretKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("AWSSecretKey");
            }
        }
        public static string AWSFromAddress
        {
            get
            {
                return ConfigurationHelper.GetSetting("AWSFromAddress");
            }
        }
        public static string AutoDescriptionMaxLength
        {
            get
            {
                return ConfigurationHelper.GetSetting("AutoDescriptionMaxLength");
            }
        }

        public static string PhotoExtension
        {
            get
            {
                return ConfigurationHelper.GetSetting("PhotoExtension");
            }
        }

        public static string CountOfAuctionBidsToDisplay
        {
            get
            {
                return ConfigurationHelper.GetSetting("CountOfAuctionBidsToDisplay");
            }
        }

        public static string GoogleMapsDefaultPositionLat
        {
            get
            {
                return ConfigurationHelper.GetSetting("GoogleMapsDefaultPositionLat");
            }
        }
        public static string GoogleMapsDefaultPositionLng
        {
            get
            {
                return ConfigurationHelper.GetSetting("GoogleMapsDefaultPositionLng");
            }
        }

        public static string GoogleMapsApiKey
        {
            get
            {
                return ConfigurationHelper.GetSetting("GoogleMapsApiKey");
            }
        }
    }
}
