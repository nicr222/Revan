using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using System.Collections.Generic;

namespace MidStateShuttleService.Service
{
    public class BaseDbServices<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseDbServices(ApplicationDbContext context, DbSet<T> dbset)
        {
            _dbContext = context;
            _dbSet = dbset;
        }

        public virtual IEnumerable<T> GetAllEntities()
        {
            return _dbSet.ToList();
        }

        public virtual T GetEntityById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void AddEntity(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual void UpdateEntity(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual void DeleteEntity(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}