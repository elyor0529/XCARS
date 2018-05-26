using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Model;

namespace XCars.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private XCarsEntities _dataContext;
        public XCarsEntities Get()
        {
            return _dataContext ?? (_dataContext = new XCarsEntities());
        }
        protected override void DisposeCore()
        {
            _dataContext?.Dispose();
        }
    }
}
