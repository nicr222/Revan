using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class RegisterServices : BaseDbServices<RegisterModel>
    {
        public RegisterServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.RegisterModels)
        {

        }
    }
}
