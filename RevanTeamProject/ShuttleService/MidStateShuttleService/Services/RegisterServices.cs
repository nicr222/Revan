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
        

        public List<RegisterModel> GetEmailsByRoute(string pickUpId, string dropOffId)
        {
            return _dbSet.Where(x => x.SelectedRouteDetail == pickUpId || x.ReturnSelectedRouteDetail == dropOffId).ToList();
        }
    }
}
