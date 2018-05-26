using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoService : BaseService<Auto>, IAutoService
    {
        public IAutoIndexService AutoIndexService { get; set; }
        public IAutoFavoriteService AutoFavoriteService { get; set; }
        public IScheduledEmailService ScheduledEmailService { get; set; }
        public ICurrencyService CurrencyService { get; set; }

        public AutoService(IAutoRepository autoRepository, 
                           IUnitOfWork unitOfWork)
            : base(autoRepository, unitOfWork)
        {
        }

        public Auto GetPublishedByID(int id)
        {
            Auto auto = this._repository.GetAny().FirstOrDefault(a => a.ID == id && a.StatusID == 2);
            //if (auto != null && auto.DateExpires < DateTime.Now)
            //{
            //    MoveToArchives(auto);
            //    return null;
            //}

            return auto;
        }

        public void Create(Auto model)
        {
            double currencyRate = CurrencyService.GetCurrencyRate();
            model.PriceUSDSearch = (model.PriceUSD != null) ? (int)model.PriceUSD : (int)(model.PriceUAH / currencyRate);
            model.PriceUAHSearch = (model.PriceUAH != null) ? (int)model.PriceUAH : (int)(model.PriceUSD * currencyRate);

            this._repository.Add(model);
            Save();
            AutoIndexService.AddToIndex(model);
        }

        public void Edit(Auto model)
        {
            double currencyRate = CurrencyService.GetCurrencyRate();
            model.PriceUSDSearch = (model.PriceUSD != null) ? (int)model.PriceUSD : (int)(model.PriceUAH / currencyRate);
            model.PriceUAHSearch = (model.PriceUAH != null) ? (int)model.PriceUAH : (int)(model.PriceUSD * currencyRate);

            this._repository.Update(model);
            Save();
            AutoIndexService.UpdateIndex(model);
        }

        public void EditMany(IEnumerable<Auto> autos)
        {
            foreach (var item in autos)
            {
                this._repository.Update(item);
            }
            Save();
        }

        public void MoveExpiredAutosToArchives()
        {
            List<Auto> autos = this._repository.GetAny().Where(a => a.StatusID == 2 && a.DateExpires < DateTime.Now).ToList();
            for (int i = 0; i < autos.Count; i++)
            {
                autos[i].StatusID = 3;
                List<AutoFavorite> favs = autos[i].AutoFavorites.ToList();
                for (int j = 0; j < favs.Count; j++)
                    AutoFavoriteService.Delete(favs[j]);
            }

            EditMany(autos);
        }

        public void Publish(Auto model, DateTime dateExpires)
        {
            model.StatusID = 2;
            model.DateExpires = dateExpires;
            //model.DateExpires = DateTime.Now.AddMinutes(2);
            model.DatePublished = DateTime.Now;
            model.DateAppearance = DateTime.Now;

            Edit(model);
        }

        public void MoveToArchives(Auto model, bool movedManually = false)
        {
            model.StatusID = 3;
            model.Top = 0;

            List<AutoFavorite> favs = model.AutoFavorites.ToList();
            for (int i = 0; i < favs.Count; i++)
                AutoFavoriteService.Delete(favs[i]);

            if (movedManually)
            {
                //cancel scheduled emails concerning this auto
                ScheduledEmailService.CancelScheduledEmails(4, model.ID);
            }

            Edit(model);
        }

        public void Delete(Auto model)
        {
            model.StatusID = 4;

            List<AutoFavorite> favs = model.AutoFavorites.ToList();
            for (int i = 0; i < favs.Count; i++)
                AutoFavoriteService.Delete(favs[i]);

            Edit(model);
            AutoIndexService.DeleteFromIndex(model);
        }

        public IEnumerable<Auto> GetAllPublished()
        {
            return _repository.GetMany(a => a.StatusID == 2 /*&& a.DateExpires > DateTime.Now*/)/*.OrderByDescending(a => a.DatePublished)*/;
        }

        public IEnumerable<Auto> GetRelated(int id)
        {
            Auto auto = GetPublishedByID(id);
            if (auto == null)
                return new List<Auto>();

            return GetAllPublished()
                            .Where(a => a.ID != id
                                    && a.MakeID == auto.MakeID 
                                    && a.ModelID == auto.ModelID 
                                    && a.YearOfIssue == auto.YearOfIssue);
        }

        public List<object> GetAllActive()
        {
            List<object> result = new List<object>();
            var autos = _repository.GetMany(a => a.StatusID == 2)
                .Select(item => new
                {
                    ID = item.ID,
                    Deadline = item.DateExpires
                });

            foreach (var item in autos)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
