using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCars.Model
{
    public partial class XCarsEntities
    {
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
