using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using System.Data;
using System.Xml;

namespace MidStateShuttleService.Service
{
    public class RouteServices : BaseDbServices<Routes>
    {
        public RouteServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Routes) { 

        }
    }
}
