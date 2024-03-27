using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;

        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public LocationController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        // GET: LocationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }
            else
            {


                TempData["SuccessMessage"] = "The location has been successfully created!";

            }

            LocationServices ls = new LocationServices(_context);
            ls.AddEntity(location);

            return RedirectToAction("Index", "Home"); // Assuming "Home" is the controller where you want to redirect
        }


        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
