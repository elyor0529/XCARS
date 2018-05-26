using System.Collections.Generic;
using System.Web.Mvc;

namespace XCars.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        T GetByID(int id);
        IEnumerable<T> GetAll();
    }
}
