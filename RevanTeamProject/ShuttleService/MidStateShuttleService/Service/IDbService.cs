using MidStateShuttleService.Models.Data;
using System.Xml;

namespace MidStateShuttleService.Service
{
    public interface IDbService<T>
    {
        IEnumerable<T> GetAllEntities();
        T GetEntityById(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(int id);
    }
}
