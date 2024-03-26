using MidStateShuttleService.Models;
using Route = MidStateShuttleService.Models;

namespace MidStateShuttleService.Service
{
    public interface IListService
    {
        IEnumerable<Location> GetLocationList();

        IEnumerable<Routes> GetRoutes();

        IEnumerable<Bus> GetBusList();

        IEnumerable<Driver> GetDriverList();
    }
}
