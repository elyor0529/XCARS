using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XCars.Model;
using XCars.Data.Infrastructure;
using XCars.Data.Repository;
using XCars.Service.Interfaces;
using XCars.Common;

namespace XCars.Service
{
    public class AuctionIndexService : BaseService<Auction>, IAuctionIndexService
    {
        public ICityService CityService { get; set; }
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }
        public IAutoMakeService AutoMakeService { get; set; }
        public IAutoModelService AutoModelService { get; set; }
        public IAutoTransmissionTypeService AutoTransmissionTypeService { get; set; }
        public IAutoDriveTypeService AutoDriveTypeService { get; set; }
        public IAutoColorService AutoColorService { get; set; }
        public IAutoFuelTypeService AutoFuelTypeService { get; set; }
        public IAutoStatusService AutoStatusService { get; set; }
        public IAuctionStatusService AuctionStatusService { get; set; }
        public IHttpWebService HttpWebService { get; set; }
        public IAutoTSRegistrationService AutoTSRegistrationService { get; set; }

        public AuctionIndexService()
            : base(null, null)
        {
        }


        public void AddToIndex(Auction auction)
        {
            try
            {
                List<Tuple<string, string, string>> additionalFields = new List<Tuple<string, string, string>>()
                {
                    new Tuple<string, string, string>("InFavorites", auction.AuctionFavorites.Count.ToString(), "int")
                };

                var postData = BuildPostData(auction, additionalFields);
                var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndexAuction}" + $"/{XCarsConfiguration.AmazonESDomenIndexMappingAuction}/" + auction.ID, "PUT", postData);
            }
            catch (Exception ex)
            { }
        }

        public void UpdateIndex(Auction auction)
        {
            try
            {
                AddToIndex(auction);
            }
            catch (Exception ex)
            { }
        }

        public void DeleteFromIndex(Auction auction)
        {
            try
            {
                var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndexAuction}" + $"/{XCarsConfiguration.AmazonESDomenIndexMappingAuction}/" + auction.ID, "DELETE");
            }
            catch (Exception ex)
            { }
        }

        public void UpdatePriceForAllAuctionsInIndex(double rate)
        {
            //int start = 0;

            //int length = 10000;
            //int.TryParse(XCarsConfiguration.AmazonESDomenIndexBulkUpdateMaxLength, out length);

            //Dictionary<int, List<Tuple<string, string, string>>> param = new Dictionary<int, List<Tuple<string, string, string>>>();

            //bool entitiesExist = true;
            //while (entitiesExist)
            //{
            //    var postData = @"{
            //        ""from"": " + start + @", 
            //        ""size"" : " + length + @",
	           //     ""_source"": [
            //            ""AutoID"",
            //            ""PriceUSD"",
		          //      ""PriceUAH""
	           //     ]";
            //    postData += @",
	           //     ""query"":
	           //     {
            //            ""bool"":
		          //      {
            //                ""must"":
            //                [ ],
			         //       ""filter"":
			         //       [
				        //        {""term"": {""AutoStatus.ID"": 2 }}
			         //       ]
		          //      }
	           //     }
            //    }";

            //    var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_search", "POST", postData);
            //    var reader = new StreamReader(response.GetResponseStream());
            //    JavaScriptSerializer js = new JavaScriptSerializer();
            //    var objText = reader.ReadToEnd();
            //    var responseJSON = (dynamic)js.Deserialize<dynamic>(objText);

            //    int total = 0;
            //    foreach (var item in responseJSON["hits"]["hits"])
            //    {
            //        int autoID = 0;
            //        int.TryParse(item["_source"]["AutoID"].ToString(), out autoID);
            //        if (autoID == 0)
            //            continue;

            //        int priceUSDSearch = 0;
            //        int priceUAHSearch = 0;

            //        string priceUSDStr = null;
            //        string priceUAHStr = null;
            //        foreach (var property in item["_source"].Keys)
            //        {
            //            if (property == "PriceUSD")
            //                priceUSDStr = item["_source"]["PriceUSD"].ToString();
            //            if (property == "PriceUAH")
            //                priceUAHStr = item["_source"]["PriceUAH"].ToString();
            //        }

            //        if (!string.IsNullOrWhiteSpace(priceUSDStr) && priceUSDStr != "0")
            //            int.TryParse(priceUSDStr, out priceUSDSearch);
            //        else if (!string.IsNullOrWhiteSpace(priceUAHStr) && priceUAHStr != "0")
            //        {
            //            int.TryParse(priceUAHStr, out priceUSDSearch);
            //            double tmp = priceUSDSearch;
            //            tmp = tmp / rate;
            //            priceUSDSearch = (int)tmp;
            //        }

            //        if (!string.IsNullOrWhiteSpace(priceUAHStr) && priceUAHStr != "0")
            //            int.TryParse(priceUAHStr, out priceUAHSearch);
            //        else if (!string.IsNullOrWhiteSpace(priceUSDStr) && priceUSDStr != "0")
            //        {
            //            int.TryParse(priceUSDStr, out priceUAHSearch);
            //            double tmp = priceUAHSearch;
            //            tmp = tmp * rate;
            //            priceUAHSearch = (int)tmp;
            //        }

            //        param[autoID] = new List<Tuple<string, string, string>>();
            //        param[autoID].Add(new Tuple<string, string, string>("PriceUSDSearch", priceUSDSearch.ToString(), "int"));
            //        param[autoID].Add(new Tuple<string, string, string>("PriceUAHSearch", priceUAHSearch.ToString(), "int"));

            //        total++;
            //    }

            //    if (total < length)
            //        entitiesExist = false;
            //    start += total;
            //}

            //UpdateCustomFields(param);
        }

        public void UpdateCustomFields(Dictionary<int, List<Tuple<string, string, string>>> param)
        {
//            int length = 10000;
//            int.TryParse(XCarsConfiguration.AmazonESDomenIndexBulkUpdateMaxLength, out length);

//            string postData = @"";
//            string quot = "";

//            int j = 0;
//            foreach (var item in param)
//            {
//                postData += @"{ ""update"" : {""_id"" : """ + item.Key + @""", ""_type"" : """ + XCarsConfiguration.AmazonESDomenIndexMapping + @""", ""_index"" : """ + XCarsConfiguration.AmazonESDomenIndex + @"""} }
//";
//                postData += @"{ ""doc"" : {";

//                int i = 0;
//                foreach (var listItem in item.Value)
//                {
//                    if (i > 0)
//                        postData += ",";

//                    quot = @"""";
//                    if (listItem.Item3 == "int" || listItem.Item3 == "bool")
//                        quot = @"";
//                    postData += @"""" + listItem.Item1 + @""" : " + quot + @"" + listItem.Item2 + quot;
//                    i++;
//                }

//                postData += @"} }
//";
//                j++;
//                if (j == length)
//                {
//                    HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_bulk", "POST", postData);
//                    j = 0;
//                    postData = "";
//                }
//            }

//            if (j > 0)
//                HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_bulk", "POST", postData);
        }

        public object GetSingleShortFromIndex(int id)
        {
            //TO-DO
            return null;
        }

        public IEnumerable<object> GetManyFromIndex()
        {
            //TO-DO
            return null;
        }

        private string BuildPostData(Auction auction, List<Tuple<string, string, string>> additionalFields = null)
        {
            string elasticSearchDateFormat = $"{XCarsConfiguration.ElasticSearchDateFormat}";
            string fullInfo = "";

            string postData = @"{
            ""ID"": " + auction.ID + @"
            ,""AutoID"": " + auction.AutoID + @"
            ,""UserID"": " + auction.Auto.UserID + @"
            ,""DateCreated"": """ + auction.DateCreated.ToString(elasticSearchDateFormat) + @"""
            ,""Deadline"": """ + auction.Deadline.ToString(elasticSearchDateFormat) + @"""
            ,""DateUpdated"": """ + auction.Deadline.ToString(elasticSearchDateFormat) + @"""";

            int mainPhotoID = 0;
            AutoPhoto photo = auction.Auto.AutoPhotoes.FirstOrDefault(p => p.IsMain);
            if (photo != null)
                mainPhotoID = photo.ID;
            postData += @",""MainPhotoID"": " + mainPhotoID + @"";

            string name = AuctionStatusService.GetByID(auction.StatusID).Name;
            postData += @"
            ,""Status"": {
                ""ID"": " + auction.StatusID + @",
                ""Name"": """ + name + @"""
            }";

            //City city = CityService.GetByID(auction.RegionID);
            //if (city != null)
            //{
            //    name = city.Region.Name;
            //    fullInfo += name + " ";
            //    postData += @",""AutoRegion"": {
            //        ""ID"": " + auction.RegionID + @",
            //        ""Name"": """ + name + @"""
            //    }";

            //    name = city.Name;
            //    fullInfo += name + " ";
            //    postData += @",""AutoCity"": {
            //        ""ID"": " + auction.RegionID + @",
            //        ""Name"": """ + name + @"""
            //    }";
            //}

            name = AutoTransportTypeService.GetByID(auction.Auto.TransportTypeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoTransportType"": {
                ""ID"": " + auction.Auto.TransportTypeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoBodyTypeService.GetByID(auction.Auto.BodyTypeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoBodyType"": {
                ""ID"": " + auction.Auto.BodyTypeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoMakeService.GetByID(auction.Auto.MakeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoMake"": {
                ""ID"": " + auction.Auto.MakeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoModelService.GetByID(auction.Auto.ModelID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoModel"": {
                ""ID"": " + auction.Auto.ModelID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoTSRegistrationService.GetByID(auction.Auto.TSRegistrationID).NameFull;
            fullInfo += name + " ";
            postData += @"
            ,""AutoTSRegistration"": {
                ""ID"": " + auction.Auto.TSRegistrationID + @",
                ""Name"": """ + name + @"""
            }";

            postData += @",""YearOfIssue"": " + auction.Auto.YearOfIssue + @"";

            if (!string.IsNullOrWhiteSpace(auction.Auto.Modification))
            {
                postData += @",""Modification"": """ + auction.Auto.Modification + @"""";
                fullInfo += auction.Auto.Modification + " ";
            }

            postData += @",""Probeg"": " + auction.Auto.Probeg + @"";

            postData += @",""StartPrice"": " + auction.StartPrice + @"";
            postData += @",""CurrentPrice"": " + auction.CurrentPrice + @"";

            postData += @",""PriceUSDSearch"": " + auction.PriceUSDSearch + @"";
            postData += @",""PriceUAHSearch"": " + auction.PriceUAHSearch + @"";

            if (auction.Auto.TransmissionTypeID != null)
            {
                name = AutoTransmissionTypeService.GetByID((int)auction.Auto.TransmissionTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoTransmissionType"": {
                        ""ID"": " + auction.Auto.TransmissionTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            if (auction.Auto.DriveTypeID != null)
            {
                name = AutoDriveTypeService.GetByID((int)auction.Auto.DriveTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoDriveType"": {
                        ""ID"": " + auction.Auto.DriveTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            //if (auction.ColorID != null)
            //{
            //    name = AutoColorService.GetByID((int)auction.ColorID).Name;
            //    fullInfo += name + " ";
            //    postData += @"
            //        ,""AutoColor"": {
            //            ""ID"": " + auction.ColorID + @",
            //            ""Name"": """ + name + @"""
            //        }";
            //}

            //boolValue = "false";
            //if (auction.IsColorMetallic)
            //    boolValue = "true";
            //postData += @",""IsColorMetallic"": " + boolValue;

            if (auction.Auto.FuelTypeID != null)
            {
                name = AutoFuelTypeService.GetByID((int)auction.Auto.FuelTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoFuelType"": {
                        ""ID"": " + auction.Auto.FuelTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            //if (auction.Auto.FuelConsumptionCity != null)
            //{
            //    string tmpDoubleVar = auction.FuelConsumptionCity.ToString();
            //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
            //    postData += @",""FuelConsumptionCity"": " + tmpDoubleVar + @"";
            //}


            //if (auction.FuelConsumptionHighway != null)
            //{
            //    string tmpDoubleVar = auction.FuelConsumptionHighway.ToString();
            //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
            //    postData += @",""FuelConsumptionHighway"": " + tmpDoubleVar + @"";
            //}

            //if (auction.FuelConsumptionMixed != null)
            //{
            //    string tmpDoubleVar = auction.FuelConsumptionMixed.ToString();
            //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
            //    postData += @",""FuelConsumptionMixed"": " + tmpDoubleVar + @"";
            //}

            if (auction.Auto.EngineCapacity != null)
            {
                string tmpDoubleVar = auction.Auto.EngineCapacity.ToString();
                tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                postData += @",""EngineCapacity"": " + tmpDoubleVar + @"";
            }

            if (auction.Auto.Power != null)
                postData += @",""Power"": " + auction.Auto.Power + @"";


            if (!string.IsNullOrWhiteSpace(auction.Description))
            {
                fullInfo += auction.Description + " ";
                postData += @",""Description"": """ + auction.Description + @"""";
            }


            postData += @",""Views"": " + auction.Views + @"";

            //TO-DO
            //if (auction.AutoSecurities != null && auction.AutoSecurities.Count > 0)
            //{
            //    postData += @", ""AutoSecurities"": [";
            //    int i = 0;
            //    foreach (var item in auction.AutoSecurities)
            //    {
            //        if (i > 0)
            //            postData += ",";
            //        //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
            //        postData += item.ID;
            //        i++;
            //    }
            //    postData += @"]";
            //}

            //if (auction.AutoComforts != null && auction.AutoComforts.Count > 0)
            //{
            //    postData += @", ""AutoComforts"": [";
            //    int i = 0;
            //    foreach (var item in auction.AutoComforts)
            //    {
            //        if (i > 0)
            //            postData += ",";
            //        //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
            //        postData += item.ID;
            //        i++;
            //    }
            //    postData += @"]";
            //}

            //if (auction.AutoMultimedias != null && auction.AutoMultimedias.Count > 0)
            //{
            //    postData += @", ""AutoMultimedias"": [";
            //    int i = 0;
            //    foreach (var item in auction.AutoMultimedias)
            //    {
            //        if (i > 0)
            //            postData += ",";
            //        //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
            //        postData += item.ID;
            //        i++;
            //    }
            //    postData += @"]";
            //}

            //if (auction.AutoStates != null && auction.AutoStates.Count > 0)
            //{
            //    postData += @", ""AutoStates"": [";
            //    int i = 0;
            //    foreach (var item in auction.AutoStates)
            //    {
            //        if (i > 0)
            //            postData += ",";
            //        //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
            //        postData += item.ID;
            //        i++;
            //    }
            //    postData += @"]";
            //}

            //if (auction.AutoMiscs != null && auction.AutoMiscs.Count > 0)
            //{
            //    postData += @", ""AutoMiscs"": [";
            //    int i = 0;
            //    foreach (var item in auction.AutoMiscs)
            //    {
            //        if (i > 0)
            //            postData += ",";
            //        //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
            //        postData += item.ID;
            //        i++;
            //    }
            //    postData += @"]";
            //}

            //postData += @",""Top"": " + auction.Top;

            postData += @",""FullInfo"": """ + fullInfo + @"""";

            if (additionalFields != null)
            {
                string quot = "";
                foreach (var item in additionalFields)
                {
                    quot = @"""";
                    if (item.Item3 == "int" || item.Item3 == "bool")
                        quot = @"";
                    postData += @",""" + item.Item1 + @""" : " + quot + @"" + item.Item2 + quot;
                }
            }

            postData += @"
            }";

            return postData;
        }
    }
}
