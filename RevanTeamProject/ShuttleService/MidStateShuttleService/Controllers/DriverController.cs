﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class DriverController : Controller
    {
        private readonly ILogger<DriverController> _logger;
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public DriverController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        // GET: DriverController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return View(driver);
            }
            else
            {


                TempData["SuccessMessage"] = "The driver has been successfully created!";

            }

            DriverServices ds = new DriverServices(_context);
            ds.AddEntity(driver);

            return RedirectToAction("Index", "Dashboard"); // Assuming "Home" is the controller where you want to redirect
        }
        

        // GET: DriverController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DriverController/Edit/5
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

        // GET: DriverController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DriverController/Delete/5
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
