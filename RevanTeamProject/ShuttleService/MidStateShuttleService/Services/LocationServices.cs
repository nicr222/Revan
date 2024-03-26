using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Services
{
    public class LocationServices : BaseDbServices<Location>
    {
        public LocationServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Locations)
        {

        }
    }
}
