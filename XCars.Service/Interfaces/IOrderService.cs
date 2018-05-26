using XCars.Model;
using System.Collections.Generic;

namespace XCars.Service.Interfaces
{
    public interface IOrderService : IBaseService<Order>
    {
        void Create(Order model);
        void Edit(Order model);
        void Delete(Order model);
        bool IsValid(dynamic model);
        void Process(dynamic orderVM);
    }
}