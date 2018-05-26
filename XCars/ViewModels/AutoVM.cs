using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using XCars.Resourses;
using XCars.Service.Interfaces;
using XCars.Service;
using XCars.Model;
using XCars.Common;
using System.Web.Mvc;

namespace XCars.ViewModels
{
    public class AutoMainInfoVM
    {
        public int AutoID { get; set; }

        [Required]
        [Display(Name = "Region", ResourceType = typeof(Resource))]
        public int RegionID { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Resource))]
        public int CityID { get; set; }

        [Required]
        [Display(Name = "Transport", ResourceType = typeof(Resource))]
        public int TransportTypeID { get; set; }

        [Required]
        [Display(Name = "BodyType", ResourceType = typeof(Resource))]
        public int BodyTypeID { get; set; }

        [Required]
        [Display(Name = "Make", ResourceType = typeof(Resource))]
        public int MakeID { get; set; }

        [Required]
        [Display(Name = "Model", ResourceType = typeof(Resource))]
        public int ModelID { get; set; }

        [Display(Name = "NumberOfDoors", ResourceType = typeof(Resource))]
        public int? NumberOfDoors { get; set; }

        [Display(Name = "NumberOfSeats", ResourceType = typeof(Resource))]
        public int? NumberOfSeats { get; set; }

        [Required]
        [Display(Name = "YearOfIssue", ResourceType = typeof(Resource))]
        public int YearOfIssue { get; set; }

        [Display(Name = "Modification", ResourceType = typeof(Resource))]
        public string Modification { get; set; }

        [Required]
        [Display(Name = "TSRegistration", ResourceType = typeof(Resource))]
        public int TSRegistrationID { get; set; }

        //[Display(Name = "VINCode", ResourceType = typeof(Resource))]
        //public string VINCode { get; set; }
        [Required]
        [Display(Name = "TransmissionType", ResourceType = typeof(Resource))]
        public int TransmissionTypeID { get; set; }

        [Required]
        [Display(Name = "EngineCapacity", ResourceType = typeof(Resource))]
        public decimal? EngineCapacity { get; set; }

        [Required]
        [Display(Name = "Probeg", ResourceType = typeof(Resource))]
        public int Probeg { get; set; }

        //[Required]
        [Display(Name = "PriceUSD", ResourceType = typeof(Resource))]
        public int? PriceUSD { get; set; }

        //[Required]
        [Display(Name = "PriceUAH", ResourceType = typeof(Resource))]
        public int? PriceUAH { get; set; }

        //[Required]
        //[Display(Name = "Валюта")]
        public int CurrencyID { get; set; }

        //[Required]
        //[Display(Name = "IsRastamojed", ResourceType = typeof(Resource))]
        //public bool IsRastamojed { get; set; }

        [Required]
        [Display(Name = "IsTorgAvailable", ResourceType = typeof(Resource))]
        public bool IsTorgAvailable { get; set; }

        [Required]
        [Display(Name = "IsExchangeAvailable", ResourceType = typeof(Resource))]
        public bool IsExchangeAvailable { get; set; }

        [Required]
        [Display(Name = "Color", ResourceType = typeof(Resource))]
        public int ColorID { get; set; }

        [Display(Name = "IsColorMetallic", ResourceType = typeof(Resource))]
        public bool IsColorMetallic { get; set; }

        [Required]
        [Display(Name = "FuelType", ResourceType = typeof(Resource))]
        public int FuelTypeID { get; set; }

        [Required]
        [Display(Name = "DriveType", ResourceType = typeof(Resource))]
        public int DriveTypeID { get; set; }

        public static implicit operator AutoMainInfoVM(Auto model)
        {
            ICityService CityService = DependencyResolver.Current.GetService<ICityService>();

            AutoMainInfoVM vm = new AutoMainInfoVM()
            {
                AutoID = model?.ID ?? 0,
                //RegionID = model?.RegionID ?? 0,
                CityID = model?.RegionID ?? 0,
                TransportTypeID = model?.TransportTypeID ?? 0,
                BodyTypeID = model?.BodyTypeID ?? 0,
                MakeID = model?.MakeID ?? 0,
                ModelID = model?.ModelID ?? 0,
                NumberOfDoors = model?.NumberOfDoors ?? 0,
                NumberOfSeats = model?.NumberOfSeats ?? 0,
                YearOfIssue = model?.YearOfIssue ?? 0,
                Modification = model?.Modification,
                TSRegistrationID = model?.TSRegistrationID ?? 0,
                //VINCode = model?.VINCode,
                TransmissionTypeID = model?.TransmissionTypeID ?? 0,
                EngineCapacity = model?.EngineCapacity ?? 0,
                Probeg = model?.Probeg ?? 0,
                PriceUSD = model?.PriceUSD,
                PriceUAH = model?.PriceUAH,
                CurrencyID = (model == null) ? 1 : (model.PriceUSD != null ? 1 : 2),
                //IsRastamojed = model?.IsRastamojed ?? false,
                IsTorgAvailable = model?.IsTorgAvailable ?? false,
                IsExchangeAvailable = model?.IsExchangeAvailable ?? false,
                ColorID = model?.ColorID ?? 0,
                IsColorMetallic = model?.IsColorMetallic ?? false,
                FuelTypeID = model?.FuelTypeID ?? 0,
                DriveTypeID = model?.DriveTypeID ?? 0,
            };

            if (vm.CityID > 0)
                vm.RegionID = CityService.GetByID(vm.CityID).RegionID;

            return vm;
        }
    }

    public class AutoExtraInfoVM
    {
        public int AutoID { get; set; }

        


        [Display(Name = "FuelConsumptionCity", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionCity { get; set; }

        [Display(Name = "FuelConsumptionHighway", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionHighway { get; set; }

        [Display(Name = "FuelConsumptionMixed", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionMixed { get; set; }

        
        public int? Power { get; set; }

        [Display(Name = "AddFullDescription", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        public string PlaceLat { get; set; }
        public string PlaceLng { get; set; }

        //collections
        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public List<AutoPhotoVM> AutoPhotoes { get; set; }

        [Display(Name = "Security", ResourceType = typeof(Resource))]
        public int[] AutoSecurities { get; set; }

        [Display(Name = "Comfort", ResourceType = typeof(Resource))]
        public int[] AutoComforts { get; set; }

        [Display(Name = "Multimedia", ResourceType = typeof(Resource))]
        public int[] AutoMultimedias { get; set; }

        [Display(Name = "State", ResourceType = typeof(Resource))]
        public int[] AutoStates { get; set; }

        [Display(Name = "Extra", ResourceType = typeof(Resource))]
        public int[] AutoMiscs { get; set; }

        public static implicit operator AutoExtraInfoVM(Auto model)
        {
            AutoExtraInfoVM vm = new AutoExtraInfoVM()
            {
                AutoID = model?.ID ?? 0,
                
                FuelConsumptionCity = model?.FuelConsumptionCity ?? 0,
                FuelConsumptionHighway = model?.FuelConsumptionHighway ?? 0,
                FuelConsumptionMixed = model?.FuelConsumptionMixed ?? 0,
                
                Power = model?.Power ?? 0,
                Description = model?.Description,
                PlaceLat = model.PlaceLat,
                PlaceLng = model.PlaceLng,

                AutoSecurities = model?.AutoSecurities.Select(item => item.ID).ToArray(),
                AutoComforts = model?.AutoComforts.Select(item => item.ID).ToArray(),
                AutoMultimedias = model?.AutoMultimedias.Select(item => item.ID).ToArray(),
                AutoStates = model?.AutoStates.Select(item => item.ID).ToArray(),
                AutoMiscs = model?.AutoMiscs.Select(item => item.ID).ToArray()
            };
            vm.AutoPhotoes = new List<AutoPhotoVM>();

            if (model != null)
            {
                foreach (var item in model.AutoPhotoes)
                    vm.AutoPhotoes.Add(item);
            }

            return vm;
        }
    }

    public class AutoShortInfoVM
    {
        public int ID { get; set; }

        [Display(Name = "Make", ResourceType = typeof(Resource))]
        public string Make { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource))]
        public string Model { get; set; }

        [Display(Name = "YearOfIssue", ResourceType = typeof(Resource))]
        public int YearOfIssue { get; set; }

        [Display(Name = "Город")]
        public string Region { get; set; }

        //[Display(Name = "PriceUSD", ResourceType = typeof(Resource))]
        public int? PriceUSD { get; set; }

        //[Display(Name = "PriceUAH", ResourceType = typeof(Resource))]
        public int? PriceUAH { get; set; }

        public string PriceUSDStr { get; set; }
        public string PriceUAHStr { get; set; }

        public int Price { get; set; }

        public string Currency { get; set; }

        public int Probeg { get; set; }

        public string TransmissionType { get; set; }

        public string FuelType { get; set; }

        public string Description { get; set; }

        public string FuelConsumption { get; set; }

        [Display(Name = "Owner", ResourceType = typeof(Resource))]
        public UserShortVM Owner { get; set; } = new UserShortVM();

        public AutoPhotoVM Photo { get; set; }

        [Display(Name = "CountOfFavorites", ResourceType = typeof(Resource))]
        public int CountOfFavorites { get; set; }

        //[Display(Name = "Status", ResourceType = typeof(Resource))]
        public int StatusID { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resource))]
        public string Status { get; set; }

        [Display(Name = "DateAppearance", ResourceType = typeof(Resource))]
        public DateTime? DateAppearance { get; set; }

        [Display(Name = "Views", ResourceType = typeof(Resource))]
        public int Views { get; set; }

        [Display(Name = "EngineCapacity", ResourceType = typeof(Resource))]
        public decimal? EngineCapacity { get; set; }

        [Display(Name = "TSRegistration", ResourceType = typeof(Resource))]
        public string TSRegistration { get; set; }

        [Display(Name = "Modification", ResourceType = typeof(Resource))]
        public string Modification { get; set; }

        public static implicit operator AutoShortInfoVM(Auto model)
        {
            ICurrencyService CurrencyService = DependencyResolver.Current.GetService<ICurrencyService>();
            double dollarRate = CurrencyService.GetCurrencyRate();

            AutoShortInfoVM vm = new AutoShortInfoVM()
            {
                ID = model?.ID ?? 0,
                Currency = model?.Currency?.Symbol,
                Make = model?.AutoMake.Name,
                Model = model?.AutoModel.Name,
                //Price = (model == null) ? 0 : (model.PriceUSD != null ? (int)model.PriceUSD : (model.PriceUAH != null ? (int)model.PriceUAH : 0)),
                PriceUSD = (model.PriceUSD != null) ? model.PriceUSD : (model.PriceUAH != null ? (int)(model.PriceUAH / dollarRate) : 0),
                PriceUAH = (model.PriceUAH != null) ? model.PriceUAH : (model.PriceUSD != null ? (int)(model.PriceUSD * dollarRate) : 0),
                Probeg = model?.Probeg ?? 0,
                TransmissionType = model?.AutoTransmissionType?.Name,
                Region = model?.City.Name,
                YearOfIssue = model.YearOfIssue,
                Owner = model.User,
                Photo = model.AutoPhotoes.FirstOrDefault(p => p.IsMain),
                CountOfFavorites = (model.StatusID == 2 && model.DateExpires > DateTime.Now) ? model.AutoFavorites.Count : 0,
                FuelType = model?.AutoFuelType?.Name,
                FuelConsumption = model?.FuelConsumptionMixed?.ToString() ?? "",
                Description = model?.Description,
                StatusID = model?.StatusID ?? 0,
                //Status = model?.AutoStatu.Name,
                DateAppearance = model?.DateAppearance,
                Views = model?.Views ?? 0,
                EngineCapacity = model?.EngineCapacity ?? 0,
                TSRegistration = model?.AutoTSRegistration.NameFull,
                Modification = model?.Modification
            };

            string status = Resource.NotPublished;
            if (model.StatusID == 2)
            {
                status = Resource.Published;
                if (model.DateAppearance != null)
                    status += " " + model.DateAppearance.Value.ToString("dd.MM.yy");
            }
            vm.Status = status;

            string priceFormat = XCarsConfiguration.PriceFormat;
            vm.PriceUSDStr = vm.PriceUSD.Value.ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol;
            vm.PriceUAHStr = vm.PriceUAH.Value.ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol;

            return vm;
        }
    }

    public class AutoPhotoVM
    {
        public int ID { get; set; }
        public bool IsMain { get; set; }
        //public string Src { get; set; }

        public static implicit operator AutoPhotoVM(AutoPhoto model)
        {
            //IFileManager fileManager = new FileManager();
            return new AutoPhotoVM
            {
                ID = model?.ID ?? 0,
                IsMain = model?.IsMain ?? true,
                //Src = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(fileManager.GetFile(model?.Url ?? $"{XCarsConfiguration.AutoNoPhotoUrl}"))
                //Src = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + (model?.Url ?? $"{XCarsConfiguration.AutoNoPhotoUrl}")
                //Src = "http://www.kino-teatr.ru/art/4266/56593.jpg"
            };
        }
    }

    public class AutoDetailsVM
    {
        [Display(Name = "Owner", ResourceType = typeof(Resource))]
        public UserShortVM Owner { get; set; } = new UserShortVM();

        //Main Info
        public int AutoID { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Resource))]
        public string Region { get; set; }

        [Display(Name = "TransportType", ResourceType = typeof(Resource))]
        public string TransportType { get; set; }

        [Display(Name = "BodyType", ResourceType = typeof(Resource))]
        public string BodyType { get; set; }

        [Display(Name = "Make", ResourceType = typeof(Resource))]
        public string Make { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource))]
        public string Model { get; set; }

        [Display(Name = "YearOfIssue", ResourceType = typeof(Resource))]
        public int YearOfIssue { get; set; }

        [Display(Name = "Modification", ResourceType = typeof(Resource))]
        public string Modification { get; set; }

        //[Display(Name = "VINCode", ResourceType = typeof(Resource))]
        //public string VINCode { get; set; }

        [Display(Name = "TSRegistration", ResourceType = typeof(Resource))]
        public string TSRegistration { get; set; }

        [Display(Name = "Probeg", ResourceType = typeof(Resource))]
        public int Probeg { get; set; }

        [Display(Name = "PriceUSD", ResourceType = typeof(Resource))]
        public int? PriceUSD { get; set; }

        [Display(Name = "PriceUAH", ResourceType = typeof(Resource))]
        public int? PriceUAH { get; set; }

        public string PriceUSDStr { get; set; }
        public string PriceUAHStr { get; set; }

        [Display(Name = "DateAppearance", ResourceType = typeof(Resource))]
        public DateTime? DateAppearance { get; set; }

        //[Display(Name = "IsRastamojed", ResourceType = typeof(Resource))]
        //public bool IsRastamojed { get; set; }

        [Display(Name = "IsTorgAvailable", ResourceType = typeof(Resource))]
        public bool IsTorgAvailable { get; set; }

        [Display(Name = "IsExchangeAvailable", ResourceType = typeof(Resource))]
        public bool IsExchangeAvailable { get; set; }

        //Extra Info
        [Display(Name = "TransmissionType", ResourceType = typeof(Resource))]
        public string TransmissionType { get; set; }

        [Display(Name = "DriveType", ResourceType = typeof(Resource))]
        public string DriveType { get; set; }

        [Display(Name = "NumberOfDoors", ResourceType = typeof(Resource))]
        public int? NumberOfDoors { get; set; }

        [Display(Name = "NumberOfSeats", ResourceType = typeof(Resource))]
        public int? NumberOfSeats { get; set; }

        [Display(Name = "Color", ResourceType = typeof(Resource))]
        public string Color { get; set; }
        public string ColorHex { get; set; }

        [Display(Name = "IsColorMetallic", ResourceType = typeof(Resource))]
        public bool IsColorMetallic { get; set; }

        [Display(Name = "FuelType", ResourceType = typeof(Resource))]
        public string FuelType { get; set; }

        [Display(Name = "FuelConsumptionCity", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionCity { get; set; }

        [Display(Name = "FuelConsumptionHighway", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionHighway { get; set; }

        [Display(Name = "FuelConsumptionMixed", ResourceType = typeof(Resource))]
        public decimal? FuelConsumptionMixed { get; set; }

        [Display(Name = "EngineCapacity", ResourceType = typeof(Resource))]
        public decimal? EngineCapacity { get; set; }

        public decimal? Power { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "Views", ResourceType = typeof(Resource))]
        public int Views { get; set; }

        [Display(Name = "InFavorites", ResourceType = typeof(Resource))]
        public int CountOfFavorites { get; set; }

        public string PlaceLat { get; set; }
        public string PlaceLng { get; set; }

        //collections
        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public List<AutoPhotoVM> AutoPhotoes { get; set; }

        [Display(Name = "Security", ResourceType = typeof(Resource))]
        public string[] AutoSecurities { get; set; }

        [Display(Name = "Comfort", ResourceType = typeof(Resource))]
        public string[] AutoComforts { get; set; }

        [Display(Name = "Multimedia", ResourceType = typeof(Resource))]
        public string[] AutoMultimedias { get; set; }

        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string[] AutoStates { get; set; }

        [Display(Name = "Misc", ResourceType = typeof(Resource))]
        public string[] AutoMiscs { get; set; }

        //AutoExchanges
        [Display(Name = "Region", ResourceType = typeof(Resource))]
        public List<AutoExchangeDetailsVM> AutoExchangesIncome { get; set; }

        public static implicit operator AutoDetailsVM(Auto model)
        {
            //IFileManager fileManager = new FileManager();
            ICurrencyService CurrencyService = DependencyResolver.Current.GetService<ICurrencyService>();
            double dollarRate = CurrencyService.GetCurrencyRate();

            AutoDetailsVM vm = new AutoDetailsVM()
            {
                Owner = model.User,

                //Main Info
                AutoID = model.ID,
                Region = model.City?.Name,
                TransportType = model.AutoTransportType?.Name,
                BodyType = model.AutoBodyType?.Name,
                Make = model.AutoMake?.Name,
                Model = model.AutoModel?.Name,
                YearOfIssue = model.YearOfIssue,
                Modification = model.Modification,
                //VINCode = model.VINCode,
                TSRegistration = model.AutoTSRegistration?.NameFull,
                Probeg = model.Probeg,
                PriceUSD = (model.PriceUSD != null) ? model.PriceUSD : (model.PriceUAH != null ? (int)(model.PriceUAH / dollarRate) : 0),
                PriceUAH = (model.PriceUAH != null) ? model.PriceUAH : (model.PriceUSD != null ? (int)(model.PriceUSD * dollarRate) : 0),

                DateAppearance = model?.DateAppearance,
                //IsRastamojed = model.IsRastamojed,
                IsExchangeAvailable = model.IsExchangeAvailable,

                //Extra Info
                TransmissionType = model.AutoTransmissionType?.Name,
                DriveType = model.AutoDriveType?.Name,
                NumberOfDoors = model.NumberOfDoors,
                NumberOfSeats = model.NumberOfSeats,
                Color = model.AutoColor?.Name + (model.IsColorMetallic ? " " + Resource.MetallicSmall : ""),
                ColorHex = model.AutoColor?.Hex,
                IsColorMetallic = model.IsColorMetallic,
                FuelType = model.AutoFuelType?.Name,
                FuelConsumptionCity = model.FuelConsumptionCity,
                FuelConsumptionHighway = model.FuelConsumptionHighway,
                FuelConsumptionMixed = model.FuelConsumptionMixed,
                EngineCapacity = model.EngineCapacity,
                Power = model.Power,
                Description = model.Description,
                Views = model.Views,
                CountOfFavorites = (model.StatusID == 2 && model.DateExpires > DateTime.Now) ? model.AutoFavorites.Count : 0,
                PlaceLat = model.PlaceLat,
                PlaceLng = model.PlaceLng,

                //collections
                AutoSecurities = model.AutoSecurities.Select(item => item.Name).ToArray(),
                AutoComforts = model.AutoComforts.Select(item => item.Name).ToArray(),
                AutoMultimedias = model.AutoMultimedias.Select(item => item.Name).ToArray(),
                AutoStates = model.AutoStates.Select(item => item.Name).ToArray(),
                AutoMiscs = model.AutoMiscs.Select(item => item.Name).ToArray()
            };

            string priceFormat = XCarsConfiguration.PriceFormat;
            vm.PriceUSDStr = vm.PriceUSD.Value.ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol;
            vm.PriceUAHStr = vm.PriceUAH.Value.ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol;


            //AutoPhotos
            vm.AutoPhotoes = new List<AutoPhotoVM>();
            foreach (var item in model.AutoPhotoes)
                vm.AutoPhotoes.Add(item);
            if (vm.AutoPhotoes.Count == 0)
                vm.AutoPhotoes.Add(new AutoPhotoVM()
                {
                    ID = 0,
                    IsMain = true,
                    //Src = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + $"{XCarsConfiguration.AutoNoPhotoUrl}"
                    //Src = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(fileManager.GetFile($"{XCarsConfiguration.AutoNoPhotoUrl}"))
                });

            //AutoExchanges
            vm.AutoExchangesIncome = new List<AutoExchangeDetailsVM>();
            foreach (var item in model.AutoExchangesIncome)
                vm.AutoExchangesIncome.Add(item);

            return vm;
        }
    }

    public class AutoExchangeDetailsVM
    {
        public int ID { get; set; }
        public AutoShortInfoVM Auto { get; set; }
        public DateTime DateCreated { get; set; }
        public int? DiffPrice { get; set; }
        public string Currency { get; set; }
        public int? DiffPriceDirection { get; set; }
        public bool DeleteButtonIsAvailable { get; set; }

        public static implicit operator AutoExchangeDetailsVM(AutoExchange model)
        {
            int k = 0;
            k++;
            AutoExchangeDetailsVM vm = new AutoExchangeDetailsVM()
            {
                Auto = model?.AutoOffered,
                ID = model?.ID ?? 0,
                Currency = model?.Currency?.Symbol,
                DateCreated = model?.DateCreated ?? DateTime.Now,
                DiffPrice = model?.DiffPrice,
                DiffPriceDirection = model?.DiffPriceDirection,
                //DeleteButtonIsAvailable = (currentUser != null && (currentUser.ID == auto.UserID || currentUser.ID == ex.AutoOffered.UserID))
            };

            return vm;
        }
    }

    public class AddExchangeOfferVM
    {
        public int TargetAutoID { get; set; }
        public int OfferedAutoID { get; set; }
        public int? DiffPrice { get; set; }
        public int CurrencyID { get; set; }
        public int? DiffPriceDirection { get; set; }
    }
}