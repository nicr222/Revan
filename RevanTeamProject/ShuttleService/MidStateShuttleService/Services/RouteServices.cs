using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using System.Data;
using System.Xml;

namespace MidStateShuttleService.Service
{
    public class RouteServices : BaseDbServices<Routes>
    {
        public RouteServices(ApplicationDbContext dbContext) : base(dbContext, dbContext.Routes)
        {

        }

        public List<Routes> GetRoutesByLocations(int pickUpId, int dropOffId)
        {
            return _dbSet.Where(x => x.PickUpLocationID == pickUpId && x.DropOffLocationID == dropOffId).ToList();
        }

        public List<Routes> GetScheduleRoutes()
        {
            List<Routes> routes = _dbSet.Where(r => r.IsActive == true).ToList();

            LocationServices ls = new LocationServices(_dbContext);

            // You can remove this block if you already have the required navigation properties loaded
            foreach (var route in routes)
            {
                route.PickUpLocation = ls.GetEntityById(route.PickUpLocationID);
                route.DropOffLocation = ls.GetEntityById(route.DropOffLocationID);
            }

            routes = routes.OrderBy(r => r.PickUpLocation.Name) // Then sort by PickUpLocation Name
                                         .ThenBy(r => r.PickUpTime).ToList(); // Then sort by PickUpTime

            return routes;
        }

        public List<Routes> GetConnectingRoutes(Routes route)
        {
            List<Routes> routes = GetScheduleRoutes();
            List<Routes> connectedRoutes = routes
                .Where(r => r.PickUpLocationID == route.DropOffLocationID
                        && r.PickUpTime > route.DropOffTime
                        && r.IsActive == true
                        && route.IsActive == true)
                .ToList();

            return connectedRoutes;
        }
    }
}
