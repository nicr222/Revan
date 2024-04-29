using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class RegisterServices : BaseDbServices<RegisterModel>
    {
        public RegisterServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.RegisterModels)
        {

        }

        // Retrieve registrations with matching pickup and drop-off locations
        

        public List<RegisterModel> GetEmailsByRoute(string routeId)
        {
            var mailingList = _dbSet.Where(x => x.SelectedRouteDetail == routeId || x.ReturnSelectedRouteDetail == routeId).ToList();

            return mailingList;
        }
    }
}
