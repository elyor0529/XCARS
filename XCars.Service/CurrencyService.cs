using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        public ISiteSettingsService SettingsService { get; set; }
        public IAutoIndexService AutoIndexService { get; set; }

        public CurrencyService(ICurrencyRepository currencyRepository, IUnitOfWork unitOfWork)
            : base(currencyRepository, unitOfWork)
        {
        }

        public List<SelectListItem> GetAllAsSelectList(int selected = 0)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Symbol,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public double GetCurrencyRate()
        {
            double rate = 1;

            SiteSetting currencyRate = SettingsService.GetByKey("currencyRate");
            if (currencyRate != null)
                double.TryParse(currencyRate.Value, out rate);

            if (rate < 1)
                rate = 1;

            return rate;
        }

        public void SaveCurrencyRate(double rate)
        {
            try
            {
                SiteSetting setting = SettingsService.GetByKey("currencyRate");

                if (setting == null)
                    setting = new SiteSetting() { Keyy = "currencyRate" };

                setting.Value = rate.ToString();
                if (setting.ID == 0)
                    SettingsService.Create(setting);
                else
                    SettingsService.Edit(setting);

                AutoIndexService.UpdatePriceForAllAutosInIndex(rate);
            }
            catch (Exception ex)
            { }
        }
    }
}
