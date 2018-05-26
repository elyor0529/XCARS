using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoExchangeService : IBaseService<AutoExchange>
    {
        void Create(AutoExchange model);
        void Delete(AutoExchange model);
    }
}
