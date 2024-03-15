using MidStateShuttleService.Models;
using Route = MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public interface IListService
    {
        IEnumerable<Location> GetLocationList();
    }
}
