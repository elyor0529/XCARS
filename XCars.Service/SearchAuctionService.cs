using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XCars.Common;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class SearchAuctionService : ISearchAuctionService
    {
        public IHttpWebService HttpWebService { get; set; }

        public SearchAuctionService()
        { }

        public List<dynamic> ExtendedSearch(int start, int length, dynamic searchVM, out int recordsFiltered, out string error)
        {
            string must = "";
            string must_not = "";
            string filter = "";
            string should = "";
            string sort = "";
            string minimum_should_match = "0";

            try
            {
                if (searchVM.IDsToBeExcluded != null)
                {
                    string tmp_IDs_NOT_IN = "";
                    foreach (var item in searchVM.IDsToBeExcluded)
                    {
                        tmp_IDs_NOT_IN += "," + item;
                    }
                    if (tmp_IDs_NOT_IN.Length > 0)
                        must_not += @",{""terms"": {""AutoID"": [" + tmp_IDs_NOT_IN.Substring(1, tmp_IDs_NOT_IN.Length - 1) + @"]}}";
                }

                if (searchVM.StatusID != null)
                    filter += @",{""term"": {""Status.ID"": " + searchVM.StatusID + @"}}";

                if (searchVM.UserID != null)
                    filter += @",{""term"": {""UserID"": " + searchVM.UserID + @"}}";

                string[] tmp = null;
                if (!string.IsNullOrWhiteSpace(searchVM.SearchText))
                    tmp = searchVM.SearchText.Split(' ');
                if (tmp != null)
                {
                    foreach (var item in tmp)
                    {
                        if (string.IsNullOrWhiteSpace(item))
                            continue;
                        must += @",{ ""wildcard"": { ""FullInfo"": ""*" + item + @"*""}}";
                    }
                }

                //if (searchVM.BodyTypeID != null)
                //    filter += CreateFilterTermsIn(searchVM.BodyTypeID, "AutoBodyType.ID");
                //else if (searchVM.TransportTypeID != null && searchVM.TransportTypeID != 0)
                //    filter += @",{""term"": {""AutoTransportType.ID"": " + searchVM.TransportTypeID + @"}}";

                //string filter_terms_IN = "";
                //HashSet<int> makesWithModels = new HashSet<int>();
                //if (searchVM.MakeAndModels != null)
                //{
                //    foreach (var item in searchVM.MakeAndModels)
                //    {
                //        if (item.ModelID == 0)
                //            continue;
                //        makesWithModels.Add(item.MakeID);
                //        filter_terms_IN += "," + item.ModelID;
                //    }
                //    if (filter_terms_IN.Length > 0)
                //        should += @",{""terms"": {""AutoModel.ID"": [" + filter_terms_IN.Substring(1, filter_terms_IN.Length - 1) + @"]}}";
                //}

                //if (searchVM.MakeID != null)
                //{
                //    filter_terms_IN = "";
                //    foreach (var item in searchVM.MakeID)
                //    {
                //        if (item == 0 || makesWithModels.Contains(item))
                //            continue;
                //        else
                //            filter_terms_IN += "," + item;
                //    }
                //    if (filter_terms_IN.Length > 0)
                //        should += @",{""terms"": {""AutoMake.ID"": [" + filter_terms_IN.Substring(1, filter_terms_IN.Length - 1) + @"]}}";
                //}

                //filter += CreateFilterTermsIn(searchVM.YearOfIssue, "YearOfIssue");

                //if (searchVM.YearOfIssueFrom != null && searchVM.YearOfIssueFrom != 0)
                //    filter += @",{""range"": {""YearOfIssue"": {""gte"": " + searchVM.YearOfIssueFrom + "}}}";
                //if (searchVM.YearOfIssueTo != null && searchVM.YearOfIssueTo != 0)
                //    filter += @",{""range"": {""YearOfIssue"": {""lte"": " + searchVM.YearOfIssueTo + "}}}";

                //if (searchVM.CurrencyID != null)
                //{
                //    string priceField = "PriceUSDSearch";
                //    if (searchVM.CurrencyID == 2)
                //        priceField = "PriceUAHSearch";

                //    if (searchVM.PriceFrom != null)
                //        filter += @",{""range"": {""" + priceField + @""": {""gte"": " + searchVM.PriceFrom + @"}}}";
                //    if (searchVM.PriceTo != null)
                //        filter += @",{""range"": {""" + priceField + @""": {""lte"": " + searchVM.PriceTo + @"}}}";
                //}

                //if (searchVM.ProbegFrom != null)
                //    filter += @",{""range"": {""Probeg"": {""gte"": " + searchVM.ProbegFrom + "}}}";
                //if (searchVM.ProbegTo != null)
                //    filter += @",{""range"": {""Probeg"": {""lte"": " + searchVM.ProbegTo + "}}}";

                //filter_terms_IN = "";
                //HashSet<int> regionsWithCities = new HashSet<int>();
                //if (searchVM.RegionAndCities != null)
                //{
                //    foreach (var item in searchVM.RegionAndCities)
                //    {
                //        if (item.CityID == 0)
                //            continue;
                //        regionsWithCities.Add(item.RegionID);
                //        filter_terms_IN += "," + item.CityID;
                //    }
                //    if (filter_terms_IN.Length > 0)
                //        should += @",{""terms"": {""AutoCity.ID"": [" + filter_terms_IN.Substring(1, filter_terms_IN.Length - 1) + @"]}}";
                //}

                //if (searchVM.RegionID != null)
                //{
                //    filter_terms_IN = "";
                //    foreach (var item in searchVM.RegionID)
                //    {
                //        if (item == 0 || regionsWithCities.Contains(item))
                //            continue;
                //        else
                //            filter_terms_IN += "," + item;
                //    }
                //    if (filter_terms_IN.Length > 0)
                //        should += @",{""terms"": {""AutoRegion.ID"": [" + filter_terms_IN.Substring(1, filter_terms_IN.Length - 1) + @"]}}";
                //}

                //filter += CreateFilterTermsIn(searchVM.TransmissionTypeID, "AutoTransmissionType.ID");
                //filter += CreateFilterTermsIn(searchVM.DriveTypeID, "AutoDriveType.ID");

                //if (searchVM.EngineCapacityFrom != null)
                //{
                //    string tmpDoubleVar = searchVM.EngineCapacityFrom.ToString();
                //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                //    filter += @",{""range"": {""EngineCapacity"": {""gte"": " + tmpDoubleVar + "}}}";
                //}
                //if (searchVM.EngineCapacityTo != null)
                //{
                //    string tmpDoubleVar = searchVM.EngineCapacityTo.ToString();
                //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                //    filter += @",{""range"": {""EngineCapacity"": {""lte"": " + tmpDoubleVar + "}}}";
                //}

                //filter += CreateFilterTermsIn(searchVM.FuelTypeID, "AutoFuelType.ID");

                //if (searchVM.FuelConsumptionFrom != null)
                //{
                //    string tmpDoubleVar = searchVM.FuelConsumptionFrom.ToString();
                //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                //    filter += @",{""range"": {""FuelConsumptionMixed"": {""gte"": " + tmpDoubleVar + "}}}";
                //}
                //if (searchVM.FuelConsumptionTo != null)
                //{
                //    string tmpDoubleVar = searchVM.FuelConsumptionTo.ToString();
                //    tmpDoubleVar = tmpDoubleVar.Replace(',', '.');
                //    filter += @",{""range"": {""FuelConsumptionMixed"": {""lte"": " + tmpDoubleVar + "}}}";
                //}

                //if (searchVM.PowerFrom != null)
                //    filter += @",{""range"": {""Power"": {""gte"": " + searchVM.PowerFrom + "}}}";
                //if (searchVM.PowerTo != null)
                //    filter += @",{""range"": {""Power"": {""lte"": " + searchVM.PowerTo + "}}}";

                //filter += CreateFilterTermsIn(searchVM.TSRegistrationID, "AutoTSRegistration.ID");

                //filter += CreateFilterTermsIn(searchVM.NumberOfDoors, "NumberOfDoors");

                //filter += CreateFilterTermsIn(searchVM.ColorID, "AutoColor.ID");
                //if (searchVM.IsColorMetallic != null && searchVM.IsColorMetallic)
                //    filter += @",{""term"": {""IsColorMetallic"": true}}";

                //if (searchVM.States != null)
                //{
                //    foreach (var item in searchVM.States)
                //    {
                //        if (item > 0)
                //            must += @",{""term"": {""AutoStates"": " + item + "}}";
                //    }
                //}
                //if (searchVM.Securities != null)
                //{
                //    foreach (var item in searchVM.Securities)
                //    {
                //        if (item > 0)
                //            must += @",{""term"": {""AutoSecurities"": " + item + "}}";
                //    }
                //}
                //if (searchVM.Comforts != null)
                //{
                //    foreach (var item in searchVM.Comforts)
                //    {
                //        if (item > 0)
                //            must += @",{""term"": {""AutoComforts"": " + item + "}}";
                //    }
                //}
                //if (searchVM.Multimedias != null)
                //{
                //    foreach (var item in searchVM.Multimedias)
                //    {
                //        if (item > 0)
                //            must += @",{""term"": {""AutoMultimedias"": " + item + "}}";
                //    }
                //}
                //if (searchVM.Miscs != null)
                //{
                //    foreach (var item in searchVM.Miscs)
                //    {
                //        if (item > 0)
                //            must += @",{""term"": {""AutoMiscs"": " + item + "}}";
                //    }
                //}

                //if (searchVM.IsExchangeAvailable != null && searchVM.IsExchangeAvailable)
                //    filter += @",{""term"": {""IsExchangeAvailable"": true}}";

                //if (searchVM.IsTorgAvailable != null && searchVM.IsTorgAvailable)
                //    filter += @",{""term"": {""IsTorgAvailable"": true}}";

                //if (searchVM.PeriodID != null && searchVM.PeriodID > 0)
                //{
                //    string periodFilterField = "DateCreated";
                //    if (searchVM.UserID == null)
                //        periodFilterField = "DateAppearance";

                //    int PeriodID = searchVM.PeriodID;
                //    string elasticSearchDateFormat = $"{XCarsConfiguration.ElasticSearchDateFormat}";
                //    DateTime periodFilterStarDateTime = DateTime.Now;

                //    switch (PeriodID)
                //    {
                //        case 1:
                //            periodFilterStarDateTime = periodFilterStarDateTime.AddHours(-1);
                //            break;
                //        case 2:
                //            periodFilterStarDateTime = periodFilterStarDateTime.AddHours(-6);
                //            break;
                //        case 3:
                //            periodFilterStarDateTime = DateTime.Now.Date;
                //            break;
                //        case 4:
                //            periodFilterStarDateTime = periodFilterStarDateTime.AddHours(-24);
                //            break;
                //        case 5:
                //            periodFilterStarDateTime = DateTime.Now.Date.AddDays(-7);
                //            break;
                //        case 6:
                //            periodFilterStarDateTime = DateTime.Now.Date.AddDays(-30);
                //            break;
                //        default:
                //            break;
                //    }

                //    filter += @",{ ""range"": { """ + periodFilterField + @""": { ""gte"": """ + periodFilterStarDateTime.ToString(elasticSearchDateFormat) + @"""}}}";
                //}

                //if (searchVM.WithPhotoOnly != null && searchVM.WithPhotoOnly)
                //{
                //    filter += @",{""range"": {""MainPhotoID"": {""gt"": 0}}}";
                //}

                bool sortByDefault = true;

                if (searchVM.SortID != null && searchVM.SortID > 0)
                {
                    if (searchVM.SortID == 1)
                        sort += @",{""PriceUSDSearch"":{""order"":""asc"",""missing"":""_last""}}";
                    else
                        sort += @",{""PriceUSDSearch"":{""order"":""desc"",""missing"":""_last""}}";
                    sortByDefault = false;
                }
                if (searchVM.MakeSortID != null && searchVM.MakeSortID > 0)
                {
                    if (searchVM.MakeSortID == 1)
                        sort += @",{""AutoMake.Name.keyword"":{""order"":""asc"",""missing"":""_last""}}";
                    else
                        sort += @",{""AutoMake.Name.keyword"":{""order"":""desc"",""missing"":""_last""}}";
                    sortByDefault = false;
                }
                if (searchVM.ModelSortID != null && searchVM.ModelSortID > 0)
                {
                    if (searchVM.ModelSortID == 1)
                        sort += @",{""AutoModel.Name.keyword"":{""order"":""asc"",""missing"":""_last""}}";
                    else
                        sort += @",{""AutoModel.Name.keyword"":{""order"":""desc"",""missing"":""_last""}}";
                    sortByDefault = false;
                }
                if (searchVM.YearSortID != null && searchVM.YearSortID > 0)
                {
                    if (searchVM.YearSortID == 1)
                        sort += @",{""YearOfIssue"":{""order"":""asc"",""missing"":""_last""}}";
                    else
                        sort += @",{""YearOfIssue"":{""order"":""desc"",""missing"":""_last""}}";
                    sortByDefault = false;
                }

                if (sortByDefault)
                {
                    sort += @",{""DateCreated"": {""order"": ""desc"", ""missing"" : ""_last""}}";
                    //if (searchVM.UserID == null)
                    //    sort += @",{""Top"":{""order"":""desc"",""missing"":""_last""}},{""DateAppearance"":{""order"":""desc"",""missing"":""_last""}}";
                    //else
                    //    sort += @",{""DateCreated"": {""order"": ""desc"", ""missing"" : ""_last""}}";
                }
            }
            catch (Exception ex)
            {
            }

            if (must != "")
                must = must.Substring(1, must.Length - 1);
            if (must_not != "")
                must_not = must_not.Substring(1, must_not.Length - 1);
            if (filter != "")
                filter = filter.Substring(1, filter.Length - 1);
            if (should != "")
            {
                minimum_should_match = "1";
                should = should.Substring(1, should.Length - 1);
            }
            if (sort != "")
                sort = sort.Substring(1, sort.Length - 1);

            //в запросе нужно будет указать только нужные поля ("_source"), чтобы увеличить скорость загрузки
            string postData = @"
            {
                ""from"": " + start + @", 
                ""size"" : " + length + @",
                ""query"": {
                    ""bool"": {
                        ""must"" : [" + must + @"],
                        ""must_not"" : [" + must_not + @"],
                        ""filter"": [" + filter + @"],
                        ""should"": [" + should + @"],
                        ""minimum_should_match"": " + minimum_should_match + @"
                    }
                },
                ""sort"": [" + sort + @"]
            }";

            string queryUrl = $"{XCarsConfiguration.AmazonESDomenEndpoint}" + $"/{XCarsConfiguration.AmazonESDomenIndexAuction}" + "/_search";
            var response = HttpWebService.MakeHttpWebRequest(queryUrl, "POST", postData);
            var reader = new StreamReader(response.GetResponseStream());
            JavaScriptSerializer js = new JavaScriptSerializer();
            var objText = reader.ReadToEnd();
            var responseJSON = (dynamic)js.Deserialize<dynamic>(objText);

            List<dynamic> autos = new List<dynamic>();
            foreach (var item in responseJSON["hits"]["hits"])
            {
                var src = item["_source"];
                autos.Add(new
                {
                    ID = (PropertyExists(src, "ID")) ? src["ID"].ToString() : "",
                    AutoID = (PropertyExists(src, "AutoID")) ? src["AutoID"].ToString() : "",
                    Make = (PropertyExists(src, "AutoMake")) ? src["AutoMake"]["Name"].ToString() : "",
                    Model = (PropertyExists(src, "AutoModel")) ? src["AutoModel"]["Name"].ToString() : "",
                    YearOfIssue = (PropertyExists(src, "YearOfIssue")) ? src["YearOfIssue"].ToString() : "",
                    StatusID = (PropertyExists(src, "Status")) ? src["Status"]["ID"].ToString() : "",
                    StatusName = (PropertyExists(src, "Status")) ? ((PropertyExists(src["Status"], "Name")) ? src["Status"]["Name"].ToString() : "") : "",
                    DateCreated = (PropertyExists(src, "DateCreated")) ? src["DateCreated"].ToString() : "",
                    Deadline = (PropertyExists(src, "Deadline")) ? src["Deadline"].ToString() : "",
                    MainPhotoID = (PropertyExists(src, "MainPhotoID")) ? src["MainPhotoID"].ToString() : "0",

                    PriceUSDSearch = (PropertyExists(src, "PriceUSDSearch")) ? src["PriceUSDSearch"].ToString() : "0",
                    PriceUAHSearch = (PropertyExists(src, "PriceUAHSearch")) ? src["PriceUAHSearch"].ToString() : "0",

                    Probeg = (PropertyExists(src, "Probeg")) ? src["Probeg"].ToString() : "0",
                    TransmissionType = (PropertyExists(src, "AutoTransmissionType")) ? src["AutoTransmissionType"]["Name"].ToString() : "",
                    FuelType = (PropertyExists(src, "AutoFuelType")) ? src["AutoFuelType"]["Name"].ToString() : "",
                    //FuelConsumption = (PropertyExists(src, "FuelConsumptionMixed")) ? src["FuelConsumptionMixed"].ToString() : "",
                    Views = (PropertyExists(src, "Views")) ? src["Views"].ToString() : "0",
                    InFavorites = (PropertyExists(src, "InFavorites")) ? src["InFavorites"].ToString() : "0",
                    EngineCapacity = (PropertyExists(src, "EngineCapacity")) ? src["EngineCapacity"].ToString() : "0",

                    //Region = (PropertyExists(src, "AutoCity")) ? ((PropertyExists(src["AutoCity"], "Name")) ? src["AutoCity"]["Name"].ToString() : "") : "",
                    TSRegistration = (PropertyExists(src, "AutoTSRegistration")) ? ((PropertyExists(src["AutoTSRegistration"], "Name")) ? src["AutoTSRegistration"]["Name"].ToString() : "") : "",
                    Description = (PropertyExists(src, "Description")) ? src["Description"].ToString() : "",
                    Modification = (PropertyExists(src, "Modification")) ? src["Modification"].ToString() : ""
                });
            }

            recordsFiltered = responseJSON["hits"]["total"];

            error = "";
            return autos;
        }

        public static bool PropertyExists(dynamic settings, string name)
        {
            try
            {
                var value = settings[name];
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private string CreateFilterTermsIn(int[] array, string key)
        {
            string filter = "";
            string filter_terms_IN = "";
            if (array != null && array.Length > 0 && !string.IsNullOrWhiteSpace(key))
            {
                foreach (var item in array)
                {
                    if (item == 0)
                        continue;
                    filter_terms_IN += "," + item;
                }
                if (filter_terms_IN.Length > 0)
                    filter += @",{""terms"": {""" + key + @""": [" + filter_terms_IN.Substring(1, filter_terms_IN.Length - 1) + @"]}}";
            }

            return filter;
        }
    }
}
