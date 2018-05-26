using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoExchangeService : BaseService<AutoExchange>, IAutoExchangeService
    {
        public AutoExchangeService(IAutoExchangeRepository autoExchangeRepository,
                                   IUnitOfWork unitOfWork)
            : base(autoExchangeRepository, unitOfWork)
        {
        }

        public void Create(AutoExchange model)
        {
            this._repository.Add(model);
            Save();
        }

        public void Delete(AutoExchange model)
        {
            this._repository.Delete(model);
            Save();
        }
    }
}
