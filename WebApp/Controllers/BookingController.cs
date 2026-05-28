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

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create()
        {
            var tables = _context.Set<PlayTable>().ToList();
            return View(tables);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult BookTable(string tableId, DateTime fromTime, DateTime toTime)
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
                return Forbid();

            if (!Guid.TryParse(tableId, out var tableGuid))
            {
                TempData["Error"] = "Invalid table selection";
                return RedirectToAction(nameof(Create));
            }

            // Normalize incoming times as local
            var requestedFrom = DateTime.SpecifyKind(fromTime, DateTimeKind.Local);
            var requestedTo = DateTime.SpecifyKind(toTime, DateTimeKind.Local);

            if (requestedFrom < DateTime.Now)
            {
                TempData["Error"] = "From time must be now or later.";
                return RedirectToAction(nameof(Create));
            }

            // Ensure from is less than to
            if (requestedFrom >= requestedTo)
            {
                TempData["Error"] = "End time must be after start time.";
                return RedirectToAction(nameof(Create));
            }

            var existingBookings = _context.PlayTableBookings
                .Where(b => b.PlayTableID == tableId)
                .ToList();

            var overlapping = existingBookings.Any(b =>
            {
                var existingFrom = DateTime.SpecifyKind(b.FromTime, DateTimeKind.Local);
                var existingTo = DateTime.SpecifyKind(b.ToTime, DateTimeKind.Local);
                return requestedFrom < existingTo && requestedTo > existingFrom;
            });

            if (overlapping)
            {
                TempData["Error"] = "Booking is not available due to an existing booking; please adjust the time.";
                return RedirectToAction(nameof(Create));
            }

            var table = _context.PlayTables.Find(tableGuid);
            var tableName = table?.TableName ?? tableId;

            var bookingInfo = $"{tableName} is booked to {userId} at {requestedFrom} until {requestedTo}";

            var booking = new PlayTableBooking
            {
                PlayTableID = tableId,
                UserID = userId,
                FromTime = requestedFrom,
                ToTime = requestedTo,
                BookingInfo = bookingInfo
            };

            _context.PlayTableBookings.Add(booking);
            _context.SaveChanges();

            // Preserve booking summary for the thank-you page
            TempData["BookingInfo"] = bookingInfo;

            return RedirectToAction(nameof(ResultPage));
        }

        public IActionResult ResultPage()
        {
            // Pass the booking summary (if any) as the view model
            var bookingInfo = TempData["BookingInfo"] as string;
            return View((object)bookingInfo);
        }
    }
}