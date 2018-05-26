using System;
using System.Collections.Generic;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IAutoService : IBaseService<Auto>
    {
        void Create(Auto model);

        void Edit(Auto model);

        void EditMany(IEnumerable<Auto> autos);

        void MoveExpiredAutosToArchives();

        Auto GetPublishedByID(int id);

        void Publish(Auto model, DateTime dateExpires);

        void MoveToArchives(Auto model, bool movedManually = false);

        void Delete(Auto model);

        IEnumerable<Auto> GetAllPublished();

        IEnumerable<Auto> GetRelated(int id);
        List<object> GetAllActive();
    }
}
