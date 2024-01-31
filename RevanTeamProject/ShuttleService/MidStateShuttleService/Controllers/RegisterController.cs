using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;
using System.Diagnostics;

namespace MidStateShuttleService.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Process the data here (e.g., save to database)
                // Redirect to a confirmation page or display a success message
                return RedirectToAction("Success");
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }
    }
}
