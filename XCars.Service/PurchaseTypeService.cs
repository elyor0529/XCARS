using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Common;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class PurchaseTypeService : BaseService<PurchaseType>, IPurchaseTypeService
    {
        public PurchaseTypeService(IPurchaseTypeRepository purchaseTypeRepository,
                            IUnitOfWork unitOfWork)
            : base(purchaseTypeRepository, unitOfWork)
        {
        }
    }
}