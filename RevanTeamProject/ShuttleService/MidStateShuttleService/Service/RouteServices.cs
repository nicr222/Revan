using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using System.Data;
using System.Xml;

namespace MidStateShuttleService.Service
{
    public class RouteServices : IDbService<Routes>
    {
        private readonly ApplicationDbContext _dbContext;

        public RouteServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Routes> GetAllEntities()
        {
            return _dbContext.Routes.ToList();
        }

        public Routes GetEntityById(int id)
        {
            return _dbContext.Routes.Find(id);
        }

        public void AddEntity(Routes entity)
        {
            _dbContext.Routes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Routes entity)
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
    }
}
