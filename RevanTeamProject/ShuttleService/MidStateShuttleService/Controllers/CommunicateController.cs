using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class CommunicateController : Controller
    {
        private readonly ILogger<CommunicateController> _logger;

        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public CommunicateController(ApplicationDbContext context, ILogger<CommunicateController> logger)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
            _logger = logger; // Assign the injected ILogger to the _logger field
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new CommuncateModel();
            model.LocationNames = GetLocationNames();
            return View(model);
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult Index(CommuncateModel c)
        {

            c.LocationNames = GetLocationNames();

            if (ModelState.IsValid)
            {
                try
                {
                    CommunicationServices cs = new CommunicationServices(_context);
                    cs.AddEntity(c);

                    return RedirectToAction("MessageSent");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error Sending Message");

                    return View("Error");
                }
            }

            
            return View(c);
        }

        public IActionResult MessageSent()
        {
            return View();
        }

        /// <summary>
        /// Displays the view for the student's communication form
        /// </summary>
        /// <returns> The Student Communicate View </returns>
        public IActionResult StudentCommunicate()
        {
            return View();
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult StudentCommunicate(Message c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MessageServices ms = new MessageServices(_context);
                    ms.AddEntity(c);

                    // Increment the message count in the session
                    int messageCount = HttpContext.Session.GetInt32("MessageCount") ?? 0;
                    messageCount++;

                    HttpContext.Session.SetInt32("MessageCount", messageCount);
                    // Optionally, save the last message or a summary
                    HttpContext.Session.SetString("LastMessage", "You have a new message!");

                    HttpContext.Session.SetString("CommunicationSuccess", "true"); // Using session to set Communication success.

                    TempData["CommunicationSuccess"] = true;

                    return RedirectToAction("StudentCommunicate");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error Sending Message");

                    return View("Error");
                }
            }

            return View(c);
        }

        //The method which will get the location names from the database
        private IEnumerable<SelectListItem> GetLocationNames()
        {
            LocationServices ls = new LocationServices(_context);
            var locations = ls.GetLocationNames();

            return locations;
        }
    }
}
