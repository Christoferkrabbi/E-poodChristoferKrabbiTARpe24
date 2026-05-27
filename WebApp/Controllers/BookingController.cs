using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Entities;

namespace WebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // GET /Booking -> redirect to Create
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        // GET /Booking/Create
        public IActionResult Create()
        {
            var tables = _context.Set<PlayTable>().ToList();
            return View(tables);
        }

        // POST /Booking/BookTable
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult BookTable(string tableId, DateTime fromTime, DateTime toTime)
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
                return Forbid();

            var overlapping = _context.PlayTableBookings.Any(b =>
                b.TableID == tableId &&
                (fromTime < b.ToTime && toTime > b.FromTime)
            );

            if (overlapping)
            {
                TempData["Error"] = "Booking is overlapping an already existing booking, please adjust time";
                return RedirectToAction(nameof(Create));
            }

            var bookingInfo = $"{tableId} is booked to {userId} at {fromTime} until {toTime}";

            var booking = new PlayTableBooking
            {
                TableID = tableId,
                UserID = userId,
                FromTime = fromTime,
                ToTime = toTime,
                BookingInfo = bookingInfo
            };

            _context.PlayTableBookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}