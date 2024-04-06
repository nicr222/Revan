using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using MidStateShuttleService.Service;
using System.Data;
using System.Diagnostics;

namespace MidStateShuttleService.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
            _logger = logger;
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

            // Retrieve the registration success flag and count from the session
            var registrationSuccess = HttpContext.Session.GetString("RegistrationSuccess") == "true";
            int registrationCountFromSession = HttpContext.Session.GetInt32("RegistrationCount") ?? 0;

            // You can now use registrationSuccess and registrationCountFromSession as needed
            // For instance, passing them to the view via ViewData or ViewBag, if your view logic depends on these values
            ViewData["RegistrationSuccess"] = registrationSuccess;
            ViewData["RegistrationCount"] = registrationCountFromSession;

            // Retrieve the check-in count from the session
            int checkInCountFromSession = HttpContext.Session.GetInt32("CheckInCount") ?? 0;

            // Pass it to the view
            ViewData["CheckInCount"] = checkInCountFromSession;

            // Retrieve the message count and last message from the session
            int messageCountFromSession = HttpContext.Session.GetInt32("MessageCount") ?? 0;
            string lastMessage = HttpContext.Session.GetString("LastMessage") ?? "You have a new message!";

            // Pass them to the view
            ViewData["MessageCount"] = messageCountFromSession;
            ViewData["LastMessage"] = lastMessage;


            CheckInServices cis = new CheckInServices(_context);
            allModels.CheckIn = cis.GetAllEntities();

            MessageServices ms = new MessageServices(_context);
            allModels.Message = ms.GetAllEntities();

            return View(allModels);

        }
    }
}
