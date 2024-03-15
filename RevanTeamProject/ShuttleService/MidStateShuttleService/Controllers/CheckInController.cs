using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MidStateShuttleService.Models;
using MidStateShuttleService.Service;

namespace MidStateShuttleService.Controllers
{
    public class CheckInController : Controller
    {
        private readonly ApplicationDbContext _context;

        // GET: CheckInController/Create
        public ActionResult CheckIn()
        {
            return View();
        }

        // POST: CheckInController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckIn(CheckIn checkIn)
        {
            //busid
            //get bus id buy bus number
            BusServices bs = new BusServices(_context);
            checkIn.Bus = bs.FindBusByNumber(checkIn.BusNumber);
            checkIn.BusId = checkIn.Bus.BusId;

            //routeid
            //get route id by shuttle
            RouteServices rs = new RouteServices(_context);
            

            //date
            checkIn.Date = DateTime.Now;

            //maybe set nav properties
            


            return RedirectToAction(nameof(Index));
            
        }
    }
}
