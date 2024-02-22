using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;

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
        public IActionResult Index(CommuncateModel c)
        {
            if (ModelState.IsValid)
            {
                // Retrieve passed in list of students from the database.

                // Send the message to each person registered to the shutte.
                return RedirectToAction("MessageSent");
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
                // Retrieve passed in list of students from the database.

                // Send the message to each person registered to the shutte.
                return RedirectToAction("MessageSent");
            }

            return View(c);
        }
    }
}
