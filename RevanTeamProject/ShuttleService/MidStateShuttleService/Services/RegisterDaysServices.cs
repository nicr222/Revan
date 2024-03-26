using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Service
{
    public class RegisterDaysServices : BaseDbServices<RegistertionDaysModel>
    {
        public RegisterDaysServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.RegistertionDaysModels)
        {

        }
    }
}
