using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Entities;

namespace WebApp.Controllers
{
    public class TableController : Controller
    {
        private readonly AppDbContext _context;

        public TableController(AppDbContext context)
        {
            _context = context;
        }

		public IActionResult Index()
		{
			var tables = _context.PlayTables.ToList();

			return View(tables);
		}

		public IActionResult CreateTable()
		{
			if (!User.IsInRole("Admin"))
			{
				return Forbid();
			}

			return View();
		}

		[HttpPost]
		public IActionResult CreateTable(PlayTable table)
		{
			if (!User.IsInRole("Admin"))
			{
				return Forbid();
			}

			table.Id = Guid.NewGuid();

			table.CreatedAt = DateTime.Now;
			table.ModifiedAt = DateTime.Now;
			table.LastVisitAt = DateTime.Now;

			_context.PlayTables.Add(table);

			_context.SaveChanges();

			return RedirectToAction("Index");
		}

        // GET: /Table/TableInfo?tableId={guid}
        public IActionResult TableInfo(string? tableId)
        {
            var isAuthenticated = User?.Identity?.IsAuthenticated == true;

            // Load tables including bookings so we can show bookings for the selected table
            var allTables = _context.PlayTables
                .Include(t => t.Bookings)
                .ToList();

            PlayTable? selected = null;
            if (!string.IsNullOrEmpty(tableId) && Guid.TryParse(tableId, out var guid))
            {
                selected = allTables.FirstOrDefault(t => t.Id == guid);
            }

            ViewBag.SelectedTable = selected;
            ViewBag.IsAuthenticated = isAuthenticated;

            return View(allTables);
        }
    }
}