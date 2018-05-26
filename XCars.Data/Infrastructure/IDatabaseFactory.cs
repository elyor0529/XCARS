using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCars.Model;

namespace XCars.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        XCarsEntities Get();
    }
}
