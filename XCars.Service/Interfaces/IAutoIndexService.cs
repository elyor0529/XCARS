using System.Collections.Generic;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoIndexService : IBaseService<Auto>
    {
        void AddToIndex(Auto auto);
        void UpdateIndex(Auto auto);
        void DeleteFromIndex(Auto auto);
        object GetSingleShortFromIndex(int id);
        IEnumerable<object> GetManyFromIndex();
        void UpdatePriceForAllAutosInIndex(double rate);
    }
}