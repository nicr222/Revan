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

            CheckInServices cis = new CheckInServices(_context);
            allModels.CheckIn = cis.GetAllEntities();

            MessageServices ms = new MessageServices(_context);
            allModels.Message = ms.GetAllEntities();

            return View(allModels);

        }
    }
}
