using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Service
{
    public class DriverServices : BaseDbServices<Driver>
    {
        public DriverServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Drivers)
        {

        }
    }
}
