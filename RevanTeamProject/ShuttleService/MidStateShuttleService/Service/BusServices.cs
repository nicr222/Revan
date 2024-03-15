using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public class BusServices : IDbService<Bus>
    {
        private readonly ApplicationDbContext _dbContext;

        public BusServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Bus> GetAllEntities()
        {
            return _dbContext.Buses.ToList();
        }

        public Bus GetEntityById(int id)
        {
            return _dbContext.Buses.Find(id);
        }

        public void AddEntity(Bus entity)
        {
            _dbContext.Buses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Bus entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var entity = _dbContext.Buses.Find(id);
            if (entity != null)
            {
                _dbContext.Buses.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        //find bus by bus number
        public Bus FindBusByNumber(int busNumber)
        {
            return _dbContext.Buses.FirstOrDefault(b => b.BusNo == busNumber);
        }
    }
}
