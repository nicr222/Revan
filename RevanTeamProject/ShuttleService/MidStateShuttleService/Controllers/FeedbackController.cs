using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;

namespace MidStateShuttleService.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FeedbackController> _logger;

        // Constructor to inject the database context and logger
        public FeedbackController(ApplicationDbContext context, ILogger<FeedbackController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,CustomerName,Rating")] Feedback feedback)
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving feedback.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View("Index", feedback);
        }
    }
}
