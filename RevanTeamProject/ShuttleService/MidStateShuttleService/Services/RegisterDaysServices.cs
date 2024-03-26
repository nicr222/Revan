using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Services
{
    public class RegisterDaysServices : BaseDbServices<RegistertionDaysModel>
    {
        public RegisterDaysServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.RegistertionDaysModels)
        {

        }
    }
}
