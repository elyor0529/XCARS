﻿using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;

namespace XCars.Data.Repository
{
    public class AutoExchangeRepository : RepositoryBase<AutoExchange>, IAutoExchangeRepository
    {
        public AutoExchangeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
