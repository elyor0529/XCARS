using System.Collections.Generic;
using XCars.Model;

namespace XCars.Service.Interfaces
{
    public interface IScheduledEmailService : IBaseService<ScheduledEmail>
    {
        void Create(ScheduledEmail model);
        void CancelScheduledEmails(int objectTypeID, int objectID);
    }
}
