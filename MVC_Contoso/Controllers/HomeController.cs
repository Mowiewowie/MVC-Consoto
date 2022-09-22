using Microsoft.AspNetCore.Mvc;
using MVC_Contoso.Models;
using MVC_Contoso.Services;
using System.Diagnostics;

namespace MVC_Contoso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionService _service;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ISessionService service,
            ILogger<HomeController> logger)
        {
            _service = service;
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

        public IActionResult Sessions()
        {
            _service.Initialize();
            var sessions = _service.GetSessions();
            return View(sessions);
        }


        public IActionResult SessionDetails(int roomId)
        {
            var sessionDetails = _service.GetSession(roomId);
            return View(sessionDetails);
        }

        public IActionResult SeatReservation(int roomId)
        {
            var seatReserved = _service.ReserveSeat(roomId);
            var roomDetail = _service.GetSession(roomId);

            if (seatReserved)
            {
                return View(roomDetail);
            }
            else
            {
                return RedirectToAction("SeatsUnavailable", false);
            }
        }

        public IActionResult SeatsUnavailable()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}