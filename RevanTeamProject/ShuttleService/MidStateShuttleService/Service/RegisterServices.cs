using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models.Data;

namespace MidStateShuttleService.Service
{
    public class RegisterServices : IDbService<Registration>
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Registration> GetAllEntities()
        {
            return _dbContext.Registrations.ToList();
        }

        public Registration GetEntityById(int id)
        {
            return _dbContext.Registrations.Find(id);
        }

        public void AddEntity(Registration entity)
        {
            _dbContext.Registrations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Registration entity)
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
