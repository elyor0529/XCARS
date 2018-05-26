using System;
using System.Collections.Generic;
using System.Linq;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class ScheduledEmailService : BaseService<ScheduledEmail>, IScheduledEmailService
    {
        public ScheduledEmailService(IScheduledEmailRepository scheduledEmailRepository,
                           IUnitOfWork unitOfWork)
            : base(scheduledEmailRepository, unitOfWork)
        {
        }

        public void Create(ScheduledEmail model)
        {
            model.StatusID = 1;
            this._repository.Add(model);
            Save();
        }

        public void CancelScheduledEmails(int objectTypeID, int objectID)
        {
            List<ScheduledEmail> emails = _repository.GetMany(e => e.StatusID == 1 && e.ObjectTypeID == objectTypeID && e.ObjectID == objectID).ToList();
            for (int i = 0; i < emails.Count; i++)
                emails[i].StatusID = 3;

            Save();
        }
    }
}
