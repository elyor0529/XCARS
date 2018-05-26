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
    public class AutoIndexService : BaseService<Auto>, IAutoIndexService
    {
        public ICityService CityService { get; set; }
        public IAutoTransportTypeService AutoTransportTypeService { get; set; }
        public IAutoBodyTypeService AutoBodyTypeService { get; set; }
        public IAutoMakeService AutoMakeService { get; set; }
        public IAutoModelService AutoModelService { get; set; }
        //public IYearService YearService { get; set; }
        public IAutoTransmissionTypeService AutoTransmissionTypeService { get; set; }
        public IAutoDriveTypeService AutoDriveTypeService { get; set; }
        //public IAutoDoorService AutoDoorService { get; set; }
        public IAutoColorService AutoColorService { get; set; }
        public IAutoFuelTypeService AutoFuelTypeService { get; set; }
        //public IAutoSecurityService AutoSecurityService { get; set; }
        //public IAutoComfortService AutoComfortService { get; set; }
        //public IAutoMultimediaService AutoMultimediaService { get; set; }
        //public IAutoStateService AutoStateService { get; set; }
        //public IAutoMiscService AutoMiscService { get; set; }
        //public IFileManager FileManager { get; set; }
        //public IAutoPhotoService AutoPhotoService { get; set; }
        public IAutoStatusService AutoStatusService { get; set; }
        public IHttpWebService HttpWebService { get; set; }
        public IAutoTSRegistrationService AutoTSRegistrationService { get; set; }

        public IAuctionIndexService AuctionIndexService { get; set; }

        public AutoIndexService()
            : base(null, null)
        {
        }


        public void AddToIndex(Auto auto)
        {
            try
            {
                List<Tuple<string, string, string>> additionalFields = new List<Tuple<string, string, string>>()
                {
                    new Tuple<string, string, string>("InFavorites", auto.AutoFavorites.Count.ToString(), "int")
                };

                var postData = BuildPostData(auto, additionalFields);
                var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + $"/{XCarsConfiguration.AmazonESDomenIndexMapping}/" + auto.ID, "PUT", postData);
            }
            catch (Exception ex)
            { }
        }

        public void UpdateIndex(Auto auto)
        {
            try
            {
                AddToIndex(auto);
                foreach (var item in auto.Auctions)
                    AuctionIndexService.UpdateIndex(item);
            }
            catch (Exception ex)
            { }
        }

        public void DeleteFromIndex(Auto auto)
        {
            try
            {
                var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + $"/{XCarsConfiguration.AmazonESDomenIndexMapping}/" + auto.ID, "DELETE");
            }
            catch (Exception ex)
            { }
        }

        public void UpdatePriceForAllAutosInIndex(double rate)
        {
            int start = 0;

            int length = 10000;
            int.TryParse(XCarsConfiguration.AmazonESDomenIndexBulkUpdateMaxLength, out length);

            Dictionary<int, List<Tuple<string, string, string>>> param = new Dictionary<int, List<Tuple<string, string, string>>>();

            bool entitiesExist = true;
            while (entitiesExist)
            {
                var postData = @"{
                    ""from"": " + start + @", 
                    ""size"" : " + length + @",
	                ""_source"": [
                        ""AutoID"",
                        ""PriceUSD"",
		                ""PriceUAH""
	                ]";
                postData += @",
	                ""query"":
	                {
                        ""bool"":
		                {
                            ""must"":
                            [ ],
			                ""filter"":
			                [
				                {""term"": {""Status.ID"": 2 }}
			                ]
		                }
	                }
                }";

                var response = HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_search", "POST", postData);
                var reader = new StreamReader(response.GetResponseStream());
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = reader.ReadToEnd();
                var responseJSON = (dynamic)js.Deserialize<dynamic>(objText);

                int total = 0;
                foreach (var item in responseJSON["hits"]["hits"])
                {
                    int autoID = 0;
                    int.TryParse(item["_source"]["AutoID"].ToString(), out autoID);
                    if (autoID == 0)
                        continue;

                    int priceUSDSearch = 0;
                    int priceUAHSearch = 0;

                    string priceUSDStr = null;
                    string priceUAHStr = null;
                    foreach (var property in item["_source"].Keys)
                    {
                        if (property == "PriceUSD")
                            priceUSDStr = item["_source"]["PriceUSD"].ToString();
                        if (property == "PriceUAH")
                            priceUAHStr = item["_source"]["PriceUAH"].ToString();
                    }

                    if (!string.IsNullOrWhiteSpace(priceUSDStr) && priceUSDStr != "0")
                        int.TryParse(priceUSDStr, out priceUSDSearch);
                    else if (!string.IsNullOrWhiteSpace(priceUAHStr) && priceUAHStr != "0")
                    {
                        int.TryParse(priceUAHStr, out priceUSDSearch);
                        double tmp = priceUSDSearch;
                        tmp = tmp / rate;
                        priceUSDSearch = (int)tmp;
                    }

                    if (!string.IsNullOrWhiteSpace(priceUAHStr) && priceUAHStr != "0")
                        int.TryParse(priceUAHStr, out priceUAHSearch);
                    else if (!string.IsNullOrWhiteSpace(priceUSDStr) && priceUSDStr != "0")
                    {
                        int.TryParse(priceUSDStr, out priceUAHSearch);
                        double tmp = priceUAHSearch;
                        tmp = tmp * rate;
                        priceUAHSearch = (int)tmp;
                    }

                    param[autoID] = new List<Tuple<string, string, string>>();
                    param[autoID].Add(new Tuple<string, string, string>("PriceUSDSearch", priceUSDSearch.ToString(), "int"));
                    param[autoID].Add(new Tuple<string, string, string>("PriceUAHSearch", priceUAHSearch.ToString(), "int"));

                    total++;
                }

                if (total < length)
                    entitiesExist = false;
                start += total;
            }

            UpdateCustomFields(param);
        }

        public void UpdateCustomFields(Dictionary<int, List<Tuple<string, string, string>>> param)
        {
            int length = 10000;
            int.TryParse(XCarsConfiguration.AmazonESDomenIndexBulkUpdateMaxLength, out length);

            string postData = @"";
            string quot = "";

            int j = 0;
            foreach (var item in param)
            {
                postData += @"{ ""update"" : {""_id"" : """ + item.Key + @""", ""_type"" : """ + XCarsConfiguration.AmazonESDomenIndexMapping + @""", ""_index"" : """ + XCarsConfiguration.AmazonESDomenIndex + @"""} }";
                postData += @"{ ""doc"" : {";

                int i = 0;
                foreach (var listItem in item.Value)
                {
                    if (i > 0)
                        postData += ",";

                    quot = @"""";
                    if (listItem.Item3 == "int" || listItem.Item3 == "bool")
                        quot = @"";
                    postData += @"""" + listItem.Item1 + @""" : " + quot + @"" + listItem.Item2 + quot;
                    i++;
                }

                postData += @"} }
";
                j++;
                if (j == length)
                {
                    HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_bulk", "POST", postData);
                    j = 0;
                    postData = "";
                }
            }

            if (j > 0)
                HttpWebService.MakeHttpWebRequest($"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndex}" + "/_bulk", "POST", postData);
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

        private string BuildPostData(Auto auto, List<Tuple<string, string, string>> additionalFields = null)
        {
            string elasticSearchDateFormat = $"{XCarsConfiguration.ElasticSearchDateFormat}";
            string fullInfo = "";

            string postData = @"{
            ""AutoID"": " + auto.ID + @"
            ,""UserID"": " + auto.UserID + @"
            ,""DateCreated"": """ + auto.DateCreated.ToString(elasticSearchDateFormat) + @"""
            ,""DateUpdated"": """ + auto.DateUpdated.ToString(elasticSearchDateFormat) + @"""";

            if (auto.DateExpires != null)
                postData += @",""DateExpires"": """ + auto.DateExpires.Value.ToString(elasticSearchDateFormat) + @"""";

            if (auto.DateAppearance != null)
                postData += @",""DateAppearance"": """ + auto.DateAppearance.Value.ToString(elasticSearchDateFormat) + @"""";

            //string photoUrl = XCarsConfiguration.AutoNoPhotoUrl;
            //AutoPhoto photo = auto.AutoPhotoes.FirstOrDefault(p => p.IsMain);
            //if (photo != null)
            //    photoUrl = photo.Url;
            //postData += @",""AutoPhotoUrl"": """ + photoUrl + @"""";

            int mainPhotoID = 0;
            AutoPhoto photo = auto.AutoPhotoes.FirstOrDefault(p => p.IsMain);
            if (photo != null)
                mainPhotoID = photo.ID;
            postData += @",""MainPhotoID"": " + mainPhotoID + @"";

            string name = AutoStatusService.GetByID(auto.StatusID).Name;
            postData += @"
            ,""Status"": {
                ""ID"": " + auto.StatusID + @",
                ""Name"": """ + name + @"""
            }";

            City city = CityService.GetByID(auto.RegionID);
            if (city != null)
            {
                name = city.Region.Name;
                fullInfo += name + " ";
                postData += @",""AutoRegion"": {
                    ""ID"": " + auto.RegionID + @",
                    ""Name"": """ + name + @"""
                }";

                name = city.Name;
                fullInfo += name + " ";
                postData += @",""AutoCity"": {
                    ""ID"": " + auto.RegionID + @",
                    ""Name"": """ + name + @"""
                }";
            }

            name = AutoTransportTypeService.GetByID(auto.TransportTypeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoTransportType"": {
                ""ID"": " + auto.TransportTypeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoBodyTypeService.GetByID(auto.BodyTypeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoBodyType"": {
                ""ID"": " + auto.BodyTypeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoMakeService.GetByID(auto.MakeID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoMake"": {
                ""ID"": " + auto.MakeID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoModelService.GetByID(auto.ModelID).Name;
            fullInfo += name + " ";
            postData += @"
            ,""AutoModel"": {
                ""ID"": " + auto.ModelID + @",
                ""Name"": """ + name + @"""
            }";

            name = AutoTSRegistrationService.GetByID(auto.TSRegistrationID).NameFull;
            fullInfo += name + " ";
            postData += @"
            ,""AutoTSRegistration"": {
                ""ID"": " + auto.TSRegistrationID + @",
                ""Name"": """ + name + @"""
            }";

            postData += @",""YearOfIssue"": " + auto.YearOfIssue + @"";

            if (!string.IsNullOrWhiteSpace(auto.Modification))
            {
                postData += @",""Modification"": """ + auto.Modification + @"""";
                fullInfo += auto.Modification + " ";
            }

            postData += @",""Probeg"": " + auto.Probeg + @"";

            //if (auto.PriceUSD != null)
            postData += @",""PriceUSD"": " + ((auto.PriceUSD != null) ? auto.PriceUSD : 0) + @"";
            //if (auto.PriceUAH != null)
            postData += @",""PriceUAH"": " + ((auto.PriceUAH != null) ? auto.PriceUAH : 0) + @"";

            postData += @",""PriceUSDSearch"": " + auto.PriceUSDSearch + @"";
            postData += @",""PriceUAHSearch"": " + auto.PriceUAHSearch + @"";

            string boolValue = "false";
            if (auto.IsExchangeAvailable)
                boolValue = "true";
            postData += @",""IsExchangeAvailable"": " + boolValue;

            boolValue = "false";
            if (auto.IsTorgAvailable)
                boolValue = "true";
            postData += @",""IsTorgAvailable"": " + boolValue;

            if (auto.TransmissionTypeID != null)
            {
                name = AutoTransmissionTypeService.GetByID((int)auto.TransmissionTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoTransmissionType"": {
                        ""ID"": " + auto.TransmissionTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            if (auto.DriveTypeID != null)
            {
                name = AutoDriveTypeService.GetByID((int)auto.DriveTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoDriveType"": {
                        ""ID"": " + auto.DriveTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            if (auto.NumberOfDoors != null)
                postData += @",""NumberOfDoors"": " + auto.NumberOfDoors + @"";

            if (auto.NumberOfSeats != null)
                postData += @",""NumberOfSeats"": " + auto.NumberOfSeats + @"";

            if (auto.ColorID != null)
            {
                name = AutoColorService.GetByID((int)auto.ColorID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoColor"": {
                        ""ID"": " + auto.ColorID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            boolValue = "false";
            if (auto.IsColorMetallic)
                boolValue = "true";
            postData += @",""IsColorMetallic"": " + boolValue;

            if (auto.FuelTypeID != null)
            {
                name = AutoFuelTypeService.GetByID((int)auto.FuelTypeID).Name;
                fullInfo += name + " ";
                postData += @"
                    ,""AutoFuelType"": {
                        ""ID"": " + auto.FuelTypeID + @",
                        ""Name"": """ + name + @"""
                    }";
            }

            if (auto.FuelConsumptionCity != null)
            {
                string tmpDoubleVar = auto.FuelConsumptionCity.ToString();
                tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                postData += @",""FuelConsumptionCity"": " + tmpDoubleVar + @"";
            }
                

            if (auto.FuelConsumptionHighway != null)
            {
                string tmpDoubleVar = auto.FuelConsumptionHighway.ToString();
                tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                postData += @",""FuelConsumptionHighway"": " + tmpDoubleVar + @"";
            }

            if (auto.FuelConsumptionMixed != null)
            {
                string tmpDoubleVar = auto.FuelConsumptionMixed.ToString();
                tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                postData += @",""FuelConsumptionMixed"": " + tmpDoubleVar + @"";
            }

            if (auto.EngineCapacity != null)
            {
                string tmpDoubleVar = auto.EngineCapacity.ToString();
                tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                postData += @",""EngineCapacity"": " + tmpDoubleVar + @"";
            }

            if (auto.Power != null)
                postData += @",""Power"": " + auto.Power + @"";
                

            if (!string.IsNullOrWhiteSpace(auto.Description))
            {
                fullInfo += auto.Description + " ";
                postData += @",""Description"": """ + auto.Description + @"""";
            }
                

            postData += @",""Views"": " + auto.Views + @"";

            if (auto.AutoSecurities != null && auto.AutoSecurities.Count > 0)
            {
                postData += @", ""AutoSecurities"": [";
                int i = 0;
                foreach (var item in auto.AutoSecurities)
                {
                    if (i > 0)
                        postData += ",";
                    //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
                    postData += item.ID;
                    i++;
                }
                postData += @"]";
            }

            if (auto.AutoComforts != null && auto.AutoComforts.Count > 0)
            {
                postData += @", ""AutoComforts"": [";
                int i = 0;
                foreach (var item in auto.AutoComforts)
                {
                    if (i > 0)
                        postData += ",";
                    //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
                    postData += item.ID;
                    i++;
                }
                postData += @"]";
            }

            if (auto.AutoMultimedias != null && auto.AutoMultimedias.Count > 0)
            {
                postData += @", ""AutoMultimedias"": [";
                int i = 0;
                foreach (var item in auto.AutoMultimedias)
                {
                    if (i > 0)
                        postData += ",";
                    //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
                    postData += item.ID;
                    i++;
                }
                postData += @"]";
            }

            if (auto.AutoStates != null && auto.AutoStates.Count > 0)
            {
                postData += @", ""AutoStates"": [";
                int i = 0;
                foreach (var item in auto.AutoStates)
                {
                    if (i > 0)
                        postData += ",";
                    //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
                    postData += item.ID;
                    i++;
                }
                postData += @"]";
            }

            if (auto.AutoMiscs != null && auto.AutoMiscs.Count > 0)
            {
                postData += @", ""AutoMiscs"": [";
                int i = 0;
                foreach (var item in auto.AutoMiscs)
                {
                    if (i > 0)
                        postData += ",";
                    //postData += @"{""ID"": " + item.ID + @", ""Name"": """ + item.Name + @"""}";
                    postData += item.ID;
                    i++;
                }
                postData += @"]";
            }

            postData += @",""Top"": " + auto.Top;

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
