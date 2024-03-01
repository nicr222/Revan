using MidStateShuttleService.Models;
using Route = MidStateShuttleService.Models.Data.Route;

namespace MidStateShuttleService.Service
{
    public interface IListService
    {
        IEnumerable<Location> GetLocationList();
    }
}
