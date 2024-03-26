using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Services
{
    public class CommunicationServices : BaseDbServices<CommuncateModel>
    {
        public CommunicationServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.CommuncateModels)
        {

        }
    }
}
