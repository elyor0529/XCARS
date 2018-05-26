using System.Collections.Generic;
using XCars.Data.Repository.Interfaces;
using Infrastructure = XCars.Data.Infrastructure;

namespace XCars.Service
{
    public class BaseService<T> 
        where T : class
    {
        protected IRepository<T> _repository { get; set; }

        protected Infrastructure.IUnitOfWork _unitOfWork { get; set; }

        public BaseService(IRepository<T> repository, Infrastructure.IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public virtual T GetByID(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this._repository.GetAll();
        }

        public virtual void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
