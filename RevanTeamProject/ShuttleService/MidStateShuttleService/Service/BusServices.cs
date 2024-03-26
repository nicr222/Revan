using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class BusServices : BaseDbServices<Bus>
    {
        public BusServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Buses)
        {

        }

        //find bus by bus number
        public Bus FindBusByNumber(int busNumber)
        {
            return _dbContext.Buses.FirstOrDefault(b => b.BusNo == busNumber);
        }
    }
}
