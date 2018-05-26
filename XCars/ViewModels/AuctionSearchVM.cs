using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using XCars.Resourses;

namespace XCars.ViewModels
{
    public class AuctionSearchVM
    {
        public int[] IDsToBeExcluded { get; set; }

        //public int? StateID { get; set; }

        public int? UserID { get; set; }

        [Display(Name = "SearchTextHint", ResourceType = typeof(Resource))]
        public string SearchText { get; set; }

        [Display(Name = "Make", ResourceType = typeof(Resource))]
        public int[] MakeID { get; set; }

        [Display(Name = "Model", ResourceType = typeof(Resource))]
        public int[] ModelID { get; set; }

        [Display(Name = "MakeAndModel", ResourceType = typeof(Resource))]
        public List<MakeAndModelVM> MakeAndModels { get; set; }

        [Display(Name = "Sort", ResourceType = typeof(Resource))]
        public int? SortID { get; set; }
        public int? MakeSortID { get; set; }
        public int? ModelSortID { get; set; }
        public int? YearSortID { get; set; }

        [Display(Name = "PeriodResult", ResourceType = typeof(Resource))]
        public int? PeriodID { get; set; }
    }
}