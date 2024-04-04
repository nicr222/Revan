using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext into the controller constructor
        public CheckInController(ApplicationDbContext context)
        {
            _context = context; // Assign the injected ApplicationDbContext to the _context field
        }

        // GET: CheckInController/Create
        public ActionResult CheckIn()
        {
            return View();
        }

        public ActionResult EditCheckIn(int checkInId)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(checkInId);

            if (model == null)
                return FailedCheckIn("Check In Not Found");

            return View(model);
        }

        // POST: CheckInController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn(CheckIn checkIn)
        {
            //get bus id buy bus number
            BusServices bs = new BusServices(_context);
            var busResult = bs.FindBusByNumber(checkIn.BusNumber);

            if (busResult == null)
                return FailedCheckIn("Could Not Find Shuttle");

            checkIn.Bus = busResult;
            checkIn.BusId = checkIn.Bus.BusId;


            //Need to find current route
            RouteServices rs = new RouteServices(_context);
            //if (checkIn.Bus.CurrentRouteId != null)
            // {
            //   var routeResult = rs.GetEntityById((int)checkIn.Bus.CurrentRouteId);
            //   checkIn.Route = routeResult;
            //    checkIn.RouteId = checkIn.Route.RouteID;
            // }


            //date
            checkIn.Date = DateTime.Now;
            CheckInServices cs = new CheckInServices(_context);
            cs.AddEntity(checkIn);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCheckIn(CheckIn checkIn)
        {
            CheckInServices cs = new CheckInServices(_context);
            if (checkIn == null)
                return FailedCheckIn("Updates to check in could not be applied");

            cs.UpdateEntity(checkIn);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            CheckInServices cs = new CheckInServices(_context);
            CheckIn model = cs.GetEntityById(id);

            if (model == null)
                return FailedCheckIn("Check In Not Found");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CheckIn model)
        {
            CheckInServices cs = new CheckInServices(_context);
            if (model == null)
                return FailedCheckIn("Updates to check in could not be applied");

            cs.UpdateEntity(model);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult FailedCheckIn(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View("FailedCheckIn");
        }
    }
}
