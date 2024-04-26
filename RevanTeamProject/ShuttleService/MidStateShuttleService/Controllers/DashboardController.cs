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

            CheckInServices cis = new CheckInServices(_context);
            allModels.CheckIn = cis.GetAllEntities();

            MessageServices ms = new MessageServices(_context);
            allModels.Message = ms.GetAllEntities();

            FeedbackServices fs = new FeedbackServices(_context);
            allModels.Feedback = fs.GetAllEntities();

            RegisterServices regs = new RegisterServices(_context);
            allModels.Register = regs.GetAllEntities();

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

            return View(allModels);

        }

        public ActionResult GetMessageDetails(int messageId)
        {
            // Fetch message details from the database based on the messageId
            var message = _context.Messages.Find(messageId);

            // Return a partial view with the message details
            return PartialView("_MessageDetails", message);
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
                                    .Where(p => p.ReturnSelectedRouteDetail == route.RouteID.ToString())
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
        
        // Accept and reject feedback methods
        public async Task<IActionResult> AcceptFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                feedback.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectFeedback(int id)
        {
            try
            {
                var feedback = _context.Feedbacks.Find(id);



                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();

                return RedirectToAction("Index", "Dashboard"); // Redirect to Index after successful deletion
            }
            catch (Exception ex)
            {
                // Log the SQL exception and any other exceptions
                LogEvents.LogSqlException(ex, (IWebHostEnvironment)_context);
                _logger.LogError(ex, "An error occurred while deleting feedback.");

                // Optionally add a model error for displaying an error message to the user
                ModelState.AddModelError("", "An unexpected error occurred while deleting the feedback, please try again.");

                // Return the view with an error message
                return View();
            }
        }


    }
}
