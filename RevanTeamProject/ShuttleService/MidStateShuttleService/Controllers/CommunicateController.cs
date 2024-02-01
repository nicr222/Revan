using Microsoft.AspNetCore.Mvc;

namespace MidStateShuttleService.Controllers
{
    public class CommunicateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // When the form submits, this method will play out.
        [HttpPost]
        public IActionResult SendMessage()
        {
            // Retrieve passed in list of students from the database.

            // Send the message to each person in the 

            return View();
        }
    }
}
