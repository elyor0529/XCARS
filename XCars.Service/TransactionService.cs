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
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
            : base(transactionRepository, unitOfWork)
        {
        }

        public void Create(Transaction model)
        {
            this._repository.Add(model);
            Save();
        }
    }
}