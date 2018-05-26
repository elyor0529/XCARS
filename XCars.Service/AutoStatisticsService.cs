using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoStatisticsService : IAutoStatisticsService
    {
        public IAutoService AutoService { get; set; }
        public IAutoMakeService AutoMakeService { get; set; }

        public int GetAutosCountAddedToday()
        {
            DateTime todayStart = DateTime.Now.Date;
            DateTime todayEnd = DateTime.Now.AddDays(1).Date;

            return AutoService.GetAllPublished()
                .Where(a => a.StatusID == 2 
                            && a.DateAppearance != null 
                            && a.DateAppearance.Value >= todayStart 
                            && a.DateAppearance.Value < todayEnd)
                .Count();
        }

        public List<object> GetNumberOfAutosGroupedByMake()
        {
            List<object> result = new List<object>();
            var autos = AutoService.GetAllPublished()
                .GroupBy(item => new
                {
                    MakeID = item.MakeID
                })
                .Select(item => new
                {
                    MakeID = item.Key.MakeID,
                    MakeName = item.Max(t => t.AutoMake.Name),
                    AutosNumber = item.Count()
                })
                .OrderBy(item => item.MakeName);

            foreach (var item in autos)
            {
                result.Add(item);
            }

            return result;
        }

        public List<object> GetUserAutosNumberGroupedByStatus(User user)
        {
            List<object> result = new List<object>();
            if (user != null)
            {
                var autos = AutoService.GetAll().Where(a => a.UserID == user.ID && a.StatusID != 4)
                .GroupBy(item => new
                {
                    StatusID = item.StatusID
                })
                .Select(item => new
                {
                    StatusID = item.Key.StatusID,
                    AutosNumber = item.Count()
                })
                .OrderBy(item => item.StatusID);

                foreach (var item in autos)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
