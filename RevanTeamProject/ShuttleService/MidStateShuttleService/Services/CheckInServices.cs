using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class CheckInServices : BaseDbServices<CheckIn>
    {
        public CheckInServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.CheckIns)
        {

        }
    }
}
