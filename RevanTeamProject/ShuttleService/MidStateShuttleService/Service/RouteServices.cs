using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models.Data;
using System.Data;
using System.Xml;
using Route = MidStateShuttleService.Models.Data.Route;

namespace MidStateShuttleService.Service
{
    public class RouteServices : IDbService<Route>
    {
        private readonly ApplicationDbContext _dbContext;

        public RouteServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Route> GetAllEntities()
        {
            return _dbContext.Routes.ToList();
        }

        public Route GetEntityById(int id)
        {
            return _dbContext.Routes.Find(id);
        }

        public void AddEntity(Route entity)
        {
            _dbContext.Routes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Route entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var entity = _dbContext.Routes.Find(id);
            if (entity != null)
            {
                _dbContext.Routes.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
        public Route GetCurrentRouteByBusId(int id)
        {
            Route currentRoute = null;

            // finds routes linked to the bus ID
            var routeIds = _dbContext.BusRoutes
                .Where(br => br.BusId == id)
                .Select(br => br.RouteId)
                .ToList();

            if (routeIds.Any())
            {
                // creates a list of routes
                List<Route> routes = new List<Route>();
                foreach (var routeId in routeIds)
                {
                    routes.Add(GetEntityById(routeId));
                }

                // Find the route with the pick-up time closest to the current time
                TimeOnly currentTime = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute);
                currentRoute = routes.OrderBy(route => Math.Abs(route.PickUpTime.CompareTo(currentTime))).FirstOrDefault();
            }

            return currentRoute;
        }
    }
}
