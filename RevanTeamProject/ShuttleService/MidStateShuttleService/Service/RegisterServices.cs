using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class RegisterServices : IDbService<RegisterModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<RegisterModel> GetAllEntities()
        {
            return _dbContext.RegisterModels.ToList();
        }

        public RegisterModel GetEntityById(int id)
        {
            return _dbContext.RegisterModels.Find(id);
        }

        public void AddEntity(RegisterModel entity)
        {
            _dbContext.RegisterModels.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(RegisterModel entity)
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
