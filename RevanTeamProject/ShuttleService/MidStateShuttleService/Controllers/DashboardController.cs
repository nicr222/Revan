using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public DashboardController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        public IConfiguration Configuration { get; }

        // GET: DashboardController
        public ActionResult Index()
        {
            AllModels allModels = new AllModels();

            LocationServices ls = new LocationServices(_context);
            allModels.Location = ls.GetAllEntities();

            RouteServices rs = new RouteServices(_context);
            allModels.Route = rs.GetAllEntities();

            DriverServices ds = new DriverServices(_context);
            allModels.Driver = ds.GetAllEntities();

            BusServices bs = new BusServices(_context);
            allModels.Bus = bs.GetAllEntities();

            return View(allModels);

        }

        public ActionResult PassengerList(int id)
        {
            var route = _context.Routes.FirstOrDefault(r => r.RouteID == id);

            if (route == null)
            {
                return NotFound(); // Handle the case where the route is not found
            }

            // Initialize a HashSet to store unique register IDs
            var uniqueRegisterIds = new HashSet<int>();

            // Initialize a list to store unique passengers
            var uniquePassengers = new List<RegisterModel>();

            // Fetch passengers related to this route's selected route details
            var selectedRoutePassengers = _context.RegisterModels
                                    .Where(p => p.SelectedRouteDetail == route.RouteID.ToString())
                                    .ToList();

            // Fetch passengers related to this route's return route details
            var returnRoutePassengers = _context.RegisterModels
                                    .Where(p => p.SelectedRouteDetail == route.RouteID.ToString())
                                    .ToList();

            // Add passengers from selected route details
            foreach (var passenger in selectedRoutePassengers)
            {
                if (!uniqueRegisterIds.Contains(passenger.RegistrationId))
                {
                    uniquePassengers.Add(passenger);
                    uniqueRegisterIds.Add(passenger.RegistrationId);
                }
            }

            // Add passengers from return route details
            foreach (var passenger in returnRoutePassengers)
            {
                if (!uniqueRegisterIds.Contains(passenger.RegistrationId))
                {
                    uniquePassengers.Add(passenger);
                    uniqueRegisterIds.Add(passenger.RegistrationId);
                }
            }

            // Pass the list of unique passengers and the route to the view
            ViewBag.Route = route;
            return View(uniquePassengers);
        }
    }
}
