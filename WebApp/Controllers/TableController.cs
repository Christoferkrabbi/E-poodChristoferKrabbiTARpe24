using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
	}
}