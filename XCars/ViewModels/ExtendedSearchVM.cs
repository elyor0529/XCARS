using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XCars.Resourses;

namespace XCars.ViewModels
{
    public class ExtendedSearchVM
    {
        public int[] IDsToBeExcluded { get; set; }

        public int? StatusID { get; set; }

        public int? UserID { get; set; }

        [Display(Name = "SearchTextHint", ResourceType = typeof(Resource))]
        public string SearchText { get; set; }

        [Display(Name = "TransportType", ResourceType = typeof(Resource))]
        public int? TransportTypeID { get; set; }
        //public int[] TransportTypeID { get; set; }

        [Display(Name = "BodyType", ResourceType = typeof(Resource))]
        //public int? BodyTypeID { get; set; }
        public int[] BodyTypeID { get; set; }

        [Display(Name = "Make", ResourceType = typeof(Resource))]
        public int[] MakeID { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource))]
        public int[] ModelID { get; set; }
        
        [Display(Name = "MakeAndModel", ResourceType = typeof(Resource))]
        public List<MakeAndModelVM> MakeAndModels { get; set; }

        [Display(Name = "YearOfIssue", ResourceType = typeof(Resource))]
        public int[] YearOfIssue { get; set; }

        [Display(Name = "YearOfIssue", ResourceType = typeof(Resource))]
        public int? YearOfIssueFrom { get; set; }
        public int? YearOfIssueTo { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? CurrencyID { get; set; }

        [Display(Name = "Probeg", ResourceType = typeof(Resource))]
        public int? ProbegFrom { get; set; }
        public int? ProbegTo { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Resource))]
        //public int? RegionID { get; set; }
        public int[] RegionID { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resource))]
        //public int? CityID { get; set; }
        public int[] CityID { get; set; }

        [Display(Name = "MakeAndModel", ResourceType = typeof(Resource))]
        public List<RegionAndCityVM> RegionAndCities { get; set; }

        [Display(Name = "TransmissionType", ResourceType = typeof(Resource))]
        //public int? TransmissionTypeID { get; set; }
        public int[] TransmissionTypeID { get; set; }

        [Display(Name = "DriveType", ResourceType = typeof(Resource))]
        //public int? DriveTypeID { get; set; }
        public int[] DriveTypeID { get; set; }

        [Display(Name = "EngineCapacity", ResourceType = typeof(Resource))]
        public decimal? EngineCapacityFrom { get; set; }
        public decimal? EngineCapacityTo { get; set; }

        [Display(Name = "FuelType", ResourceType = typeof(Resource))]
        //public int? FuelTypeID { get; set; }
        public int[] FuelTypeID { get; set; }

        [Display(Name = "FuelConsumption", ResourceType = typeof(Resource))]
        //public decimal? FuelConsumption { get; set; }
        public decimal? FuelConsumptionFrom { get; set; }
        public decimal? FuelConsumptionTo { get; set; }

        [Display(Name = "Power", ResourceType = typeof(Resource))]
        //public decimal? Power { get; set; }
        //public int Power { get; set; }
        public int? PowerFrom { get; set; }
        public int? PowerTo { get; set; }

        [Display(Name = "TSRegistration", ResourceType = typeof(Resource))]
        //public int? TSRegistrationID { get; set; }
        public int[] TSRegistrationID { get; set; }

        [Display(Name = "NumberOfDoors", ResourceType = typeof(Resource))]
        //public int? NumberOfDoors { get; set; }
        public int[] NumberOfDoors { get; set; }

        [Display(Name = "Color", ResourceType = typeof(Resource))]
        //public int? ColorID { get; set; }
        public int[] ColorID { get; set; }

        [Display(Name = "IsColorMetallic", ResourceType = typeof(Resource))]
        public bool IsColorMetallic { get; set; }
        

        [Display(Name = "State", ResourceType = typeof(Resource))]
        public int[] States { get; set; }

        [Display(Name = "Security", ResourceType = typeof(Resource))]
        public int[] Securities { get; set; }

        [Display(Name = "Comfort", ResourceType = typeof(Resource))]
        public int[] Comforts { get; set; }

        [Display(Name = "Multimedia", ResourceType = typeof(Resource))]
        public int[] Multimedias { get; set; }

        [Display(Name = "Extra", ResourceType = typeof(Resource))]
        public int[] Miscs { get; set; }

        [Display(Name = "IsExchangeAvailable", ResourceType = typeof(Resource))]
        public bool IsExchangeAvailable { get; set; }

        [Display(Name = "IsTorgAvailable", ResourceType = typeof(Resource))]
        public bool IsTorgAvailable { get; set; }

        [Display(Name = "Sort", ResourceType = typeof(Resource))]
        public int? SortID { get; set; }

        public int? MakeSortID { get; set; }
        public int? ModelSortID { get; set; }
        public int? YearSortID { get; set; }

        [Display(Name = "PeriodResult", ResourceType = typeof(Resource))]
        public int? PeriodID { get; set; }

        [Display(Name = "WithPhotoOnly", ResourceType = typeof(Resource))]
        public bool WithPhotoOnly { get; set; }

        public string Type { get; set; } //if == "auction" then search auctions, else search autos
    }

    public class MakeAndModelVM
    {
        public int MakeID { get; set; }

        public int ModelID { get; set; }
    }

    public class RegionAndCityVM
    {
        public int RegionID { get; set; }

        public int CityID { get; set; }
    }
}