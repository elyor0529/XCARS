using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCars.Common;
using XCars.Model;
using XCars.Resourses;
using XCars.Service;
using XCars.Service.Interfaces;

namespace XCars.ViewModels
{
    public class AuctionCreateVM
    {
        public int ID { get; set; }
        [Required]
        public int AutoID { get; set; }

        [Required]
        [Display(Name = "StartPrice", ResourceType = typeof(Resource))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EnterPositivePrice")]
        public int StartPrice { get; set; }

        [Required]
        [Display(Name = "Currency", ResourceType = typeof(Resource))]
        public int CurrencyID { get; set; }

        [Required]
        [Display(Name = "PeriodInDays", ResourceType = typeof(Resource))]
        public int Days { get; set; }

        [Required]
        [Display(Name = "PeriodInHours", ResourceType = typeof(Resource))]
        public int Hours { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public List<AuctionPhotoVM> AuctionPhotoes { get; set; }

        public static implicit operator AuctionCreateVM(Auction model)
        {
            AuctionCreateVM vm = new AuctionCreateVM()
            {
                ID = model?.ID ?? 0,
                AutoID = model?.AutoID ?? 0,
                CurrencyID = model?.CurrencyID ?? 0,
                StartPrice = model?.StartPrice ?? 0,
                Description = model?.Description
            };
            vm.AuctionPhotoes = new List<AuctionPhotoVM>();

            foreach (var item in model.AuctionPhotoes)
                vm.AuctionPhotoes.Add(item);

            return vm;
        }
    }

    public class AuctionPhotoVM
    {
        public int ID { get; set; }
        public bool IsMain { get; set; }
        //public string Src { get; set; }

        public static implicit operator AuctionPhotoVM(AuctionPhoto model)
        {
            IFileManager fileManager = new FileManager();
            return new AuctionPhotoVM
            {
                ID = model?.ID ?? 0,
                IsMain = model?.IsMain ?? true,
                //Src = $"{XCarsConfiguration.BucketEndpoint}" + $"{XCarsConfiguration.BucketName}/" + model?.Url ?? $"{XCarsConfiguration.AutoNoPhotoUrl}"
                //Src = $"{XCarsConfiguration.ImageSourceType}" + Convert.ToBase64String(fileManager.GetFile(model?.Url ?? $"{XCarsConfiguration.AutoNoPhotoUrl}"))
            };
        }
    }

    public class ApplyTermOfUseOfAuctionVM
    {
        [Required]
        [Display(Name = "AcceptTerms", ResourceType = typeof(Resource))]
        public bool Agree { get; set; }
        public int? AuctionID { get; set; }
    }

    public class AuctionDetailsVM
    {
        public int ID { get; set; }

        [Display(Name = "Auto", ResourceType = typeof(Resource))]
        //public AutoShortInfoVM Auto { get; set; }
        public AutoDetailsVM Auto { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Resource))]
        public List<AuctionPhotoVM> AuctionPhotoes { get; set; }

        [Display(Name = "Defects", ResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "DateCreated", ResourceType = typeof(Resource))]
        public System.DateTime DateCreated { get; set; }

        [Display(Name = "StartPrice", ResourceType = typeof(Resource))]
        public int StartPrice { get; set; }

        [Display(Name = "CurrentPrice", ResourceType = typeof(Resource))]
        public int CurrentPrice { get; set; }

        [Display(Name = "Currency", ResourceType = typeof(Resource))]
        public string Currency { get; set; }

        public string PriceUSDStr { get; set; }
        public string PriceUAHStr { get; set; }

        [Display(Name = "State", ResourceType = typeof(Resource))]
        public string State { get; set; }

        [Display(Name = "DateStopped", ResourceType = typeof(Resource))]
        public Nullable<System.DateTime> DateStopped { get; set; }

        [Display(Name = "Deadline", ResourceType = typeof(Resource))]
        public System.DateTime Deadline { get; set; }

        [Display(Name = "Bids", ResourceType = typeof(Resource))]
        public List<AuctionBidVM> Bids { get; set; }

        [Display(Name = "BidsCount", ResourceType = typeof(Resource))]
        public int BidsCount { get; set; }

        [Display(Name = "NumberOfParticipants", ResourceType = typeof(Resource))]
        public int NumberOfParticipants { get; set; }

        [Display(Name = "Views", ResourceType = typeof(Resource))]
        public int Views { get; set; }

        [Display(Name = "CountOfFavorites", ResourceType = typeof(Resource))]
        public int CountOfFavorites { get; set; }


        public static implicit operator AuctionDetailsVM(Auction model)
        {
            ICurrencyService CurrencyService = DependencyResolver.Current.GetService<ICurrencyService>();
            double dollarRate = CurrencyService.GetCurrencyRate();

            AuctionDetailsVM vm = new AuctionDetailsVM
            {
                ID = model?.ID ?? 0,
                Auto = model?.Auto,
                Description = model?.Description,
                Currency = model?.Currency?.Symbol,
                DateCreated = model?.DateCreated ?? DateTime.Now,
                DateStopped = model?.DateStopped,
                Deadline = model?.Deadline ?? DateTime.Now,
                StartPrice = model?.StartPrice ?? 0,
                CurrentPrice = model?.CurrentPrice ?? 0,
                State = model?.AuctionStatu.Name,
                BidsCount = model?.AuctionBids.Count ?? 0,
                Views = model?.Views ?? 0,
                CountOfFavorites = (model.StatusID == 2 && model.Deadline > DateTime.Now) ? model.AuctionFavorites.Count : 0,
            };

            string priceFormat = XCarsConfiguration.PriceFormat;

            vm.PriceUSDStr = (model.CurrencyID == 1) ? (model.StartPrice.ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol) : (((int)(model.StartPrice / dollarRate)).ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol);
            vm.PriceUAHStr = (model.CurrencyID == 2) ? (model.StartPrice.ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol) : (((int)(model.StartPrice * dollarRate)).ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol);

            List<int> biddersIDs = model.AuctionBids.Select(item => item.UserID).ToList();
            List<int> onlineAuthorizedUsersID = model.AuctionConnections.Where(cnn => cnn.UserID != null).Select(item => (int)item.UserID).ToList();
            int onlineUnauthorizedUsersCount = model.AuctionConnections.Where(cnn => cnn.UserID == null).Count();
            vm.NumberOfParticipants = biddersIDs.Union(onlineAuthorizedUsersID).Count() + onlineUnauthorizedUsersCount;


            vm.AuctionPhotoes = new List<AuctionPhotoVM>();
            foreach (var item in model.AuctionPhotoes)
                vm.AuctionPhotoes.Add(item);

            //if (vm.AuctionPhotoes.Count == 0)
            //    vm.AuctionPhotoes.Add(new AuctionPhotoVM()
            //    {
            //        ID = 0,
            //        IsMain = true
            //    });

            int lastBidsCount = 1;
            int.TryParse(XCarsConfiguration.CountOfAuctionBidsToDisplay, out lastBidsCount);

            vm.Bids = new List<AuctionBidVM>();
            foreach (var item in model.AuctionBids.OrderByDescending(item => item.ID).Take(lastBidsCount))
                vm.Bids.Add(item);

            return vm;
        }
    }

    public class AuctionShortInfoVM
    {
        public int ID { get; set; }
        public AutoShortInfoVM Auto { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated{ get; set; }
        public int StartPrice { get; set; }
        public int CurrentPrice { get; set; }
        public string PriceUSDStr { get; set; }
        public string PriceUAHStr { get; set; }
        public int CurrencyID { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public int Views { get; set; }
        public string TimeLeft { get; set; }
        [Display(Name = "CountOfFavorites", ResourceType = typeof(Resource))]
        public int CountOfFavorites { get; set; }

        public static implicit operator AuctionShortInfoVM(Auction model)
        {
            ICurrencyService CurrencyService = DependencyResolver.Current.GetService<ICurrencyService>();
            double currencyRate = CurrencyService.GetCurrencyRate();

            AuctionShortInfoVM vm = new AuctionShortInfoVM()
            {
                ID = model?.ID ?? 0,
                Auto = model?.Auto,
                Description = model?.Description,
                DateCreated = model?.DateCreated ?? DateTime.Now,
                //StartPrice = model?.StartPrice ?? 0,
                //CurrentPrice = model?.CurrentPrice ?? 0,
                CurrencyID = model?.CurrencyID ?? 0,
                StatusID = model?.StatusID ?? 0,
                //State = model?.AuctionState.Name,
                Deadline = model?.Deadline ?? DateTime.Now,
                Views = model?.Views ?? 0,
                CountOfFavorites = (model.StatusID == 2 && model.Deadline > DateTime.Now) ? model.AuctionFavorites.Count : 0,
            };

            string timeLeft = "";
            DateTime targetDT = vm.Deadline;
            int leftDaysCount = (targetDT - DateTime.Now).Days;
            if (leftDaysCount > 0)
            {
                int leftHoursCount = (targetDT.AddDays(-1 * leftDaysCount) - DateTime.Now).Hours;
                timeLeft = leftDaysCount + " " + Resource.DayShort + " " + leftHoursCount + " " + Resource.HoursShort;
            }
            else
            {
                int leftHoursCount = (targetDT - DateTime.Now).Hours;
                if (leftHoursCount > 0)
                {
                    int leftMinutesCount = (targetDT.AddHours(-1 * leftHoursCount) - DateTime.Now).Minutes;
                    timeLeft = leftHoursCount + " " + Resource.HoursShort + " " + leftMinutesCount + " " + Resource.MinutesShort;
                }
                else
                {
                    int leftMinutesCount = (targetDT - DateTime.Now).Minutes;
                    if (leftMinutesCount > 0)
                    {
                        int leftSecondsCount = (targetDT.AddMinutes(-1 * leftMinutesCount) - DateTime.Now).Seconds;
                        timeLeft = leftMinutesCount + " " + Resource.MinutesShort + " " + leftSecondsCount + " " + Resource.MinutesShort;
                    }
                    else
                    {
                        int leftSecondsCount = (targetDT - DateTime.Now).Seconds;
                        timeLeft = leftSecondsCount + " " + Resource.SecondsShort;
                    }
                }
            }
            vm.TimeLeft = timeLeft;

            string status = Resource.NotPublished;
            if (model.StatusID == 2)
                status = Resource.Published + " " + model.DateCreated.ToString("dd.MM.yy");
            vm.Status = status;

            string priceFormat = XCarsConfiguration.PriceFormat;
            int tmp = (model.CurrencyID == 1) ? model.CurrentPrice : (int)(model.CurrentPrice / currencyRate);
            vm.PriceUSDStr = tmp.ToString(priceFormat) + " " + CurrencyService.GetByID(1).Symbol;
            tmp = (model.CurrencyID == 2) ? model.CurrentPrice : (int)(model.CurrentPrice * currencyRate);
            vm.PriceUAHStr = tmp.ToString(priceFormat) + " " + CurrencyService.GetByID(2).Symbol;

            return vm;
        }
    }

    public class AuctionBidVM
    {
        public int ID { get; set; }
        public UserShortVM User { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int Amount { get; set; }

        public static implicit operator AuctionBidVM(AuctionBid model)
        {
            return new AuctionBidVM
            {
                ID = model?.ID ?? 0,
                Amount = model?.Amount ?? 0,
                DateCreated = model?.DateCreated ?? DateTime.Now,
                User = model?.User
            };
        }
    }
}