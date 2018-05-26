using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class BillingService : IBillingService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BillingService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        
        public decimal GeneratePriceForAutoPublishing(decimal oncePayedCost, int top, int days)
        {
            return oncePayedCost + top * days;
        }
    }
}
