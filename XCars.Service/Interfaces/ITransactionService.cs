using System.Collections.Generic;
using System.Web.Mvc;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface ITransactionService : IBaseService<Transaction>
    {
        void Create(Transaction model);
    }
}