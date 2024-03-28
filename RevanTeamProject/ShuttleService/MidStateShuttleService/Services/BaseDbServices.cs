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

        public virtual bool AddEntity(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                // Log success message
                Console.WriteLine("Entity added successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error occurred while adding entity: " + ex.Message);
                return false;
            }
        }

        public virtual bool UpdateEntity(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _dbContext.SaveChanges();
                // Log success message
                Console.WriteLine("Entity added successfully.");
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error occurred while adding entity: " + ex.Message);
                return false;
            }
        }

        public virtual bool DeleteEntity(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                try
                {
                    _dbSet.Remove(entity);
                    _dbContext.SaveChanges();
                    // Log success message
                    Console.WriteLine("Entity added successfully.");
                    return true;
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine("Error occurred while adding entity: " + ex.Message);
                    return false;
                }
            }

            // Log the exception
            Console.WriteLine("Entity was not found in DB");
            return false;
        }
    }
}