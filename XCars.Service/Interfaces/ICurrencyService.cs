using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface ICurrencyService : IBaseService<Currency>
    {
        List<SelectListItem> GetAllAsSelectList(int selected = 0);
        double GetCurrencyRate();
        void SaveCurrencyRate(double rate);
    }
}
