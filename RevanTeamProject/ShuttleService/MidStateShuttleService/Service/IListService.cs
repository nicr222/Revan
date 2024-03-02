using MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public interface IListService
    {
        IEnumerable<Location> GetLocationList();

        IEnumerable<Shuttle> GetShuttleList();
    }
}
