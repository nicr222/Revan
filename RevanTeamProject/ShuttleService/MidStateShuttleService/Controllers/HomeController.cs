using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Diagnostics;

namespace MidStateShuttleService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Fetch all feedback entries and order them by DateSubmitted in descending order
            //var feedbackList = _context.Feedbacks.OrderByDescending(f => f.DateSubmitted).ToList();
            //return View(feedbackList);
            // Fetch active feedback entries only
            var activeFeedbackList = _context.Feedbacks
                                      .Where(f => f.IsActive)
                                      .OrderByDescending(f => f.DateSubmitted)
                                      .ToList();
            RouteServices rs = new RouteServices(_context);
            ViewBag.RouteSchedule = rs.GetScheduleRoutes();

            return View(activeFeedbackList);
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Comment,CustomerName,Rating,DisplayTestimonial,IsActive")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if CustomerName is null or empty, and set it to "Anonymous" if it is.
                    feedback.CustomerName = string.IsNullOrWhiteSpace(feedback.CustomerName) ? "Anonymous" : feedback.CustomerName;

                    feedback.DateSubmitted = DateTime.Now; // Set submission date to current date and time
                    _context.Add(feedback);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Feedback successfully saved.");

                    TempData["FeedbackSuccess"] = "True"; // Use TempData to signal that feedback was successful
                    return RedirectToAction(nameof(Index)); // Redirect back to the form page to show the success modal
                    //return RedirectToAction("FeedbackTable");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving feedback.");
                }
            }
            else
            {
                // Debugging code to log ModelState errors
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View("Index", feedback);
        }

        private string getSchedule()
        {
            RouteServices rs = new RouteServices(_context);
            try
            {

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Routes could not be retrieved");
                return "<h5>An error has occurred displaying route schedule at this time. Please try again later.";
            }



            return null;
        }

    }

}
