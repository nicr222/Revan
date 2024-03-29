﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;
using System.Data;

namespace MidStateShuttleService.Controllers
{
    public class RoutesController : Controller
    {
        private readonly ILogger<LocationController> _logger;

        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public RoutesController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        // GET: RoutesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoutesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoutesController/Create
        public ActionResult Create()
        {
            LocationServices ls = new LocationServices(_context);
            ViewBag.Locations = ls.GetAllEntities().Select(x => new SelectListItem { Text = x.Name, Value = x.LocationId.ToString() });

            BusServices bs = new BusServices(_context);
            ViewBag.Buses = bs.GetAllEntities().Select(x => new SelectListItem { Text = "Shuttle: " + x.BusNo, Value = x.BusId.ToString() });

            return View();
        }


        // POST: RoutesController/Edit/
[HttpPost]
        public ActionResult Create(Routes route)
        {
            RouteServices rs = new RouteServices(_context);
            rs.AddEntity(route);

            return RedirectToAction("Index", "Home"); // Assuming "Home" is the controller where you want to redirect
        }


        // GET: RoutesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoutesController/Delete/5
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
