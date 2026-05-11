using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			var products = ProductStore.GetAll();
			return View(products);
		}

		[HttpGet]
		public IActionResult AddProduct()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddProduct(Product product)
		{
			if (!ModelState.IsValid)
				return View(product);

			ProductStore.Add(product);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult EditProduct(int id)
		{
			var product = ProductStore.Find(id);
			if (product == null) return NotFound();
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditProduct(Product product)
		{
			if (!ModelState.IsValid)
				return View(product);

			ProductStore.Update(product);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RemoveProduct(int id)
		{
			ProductStore.Remove(id);
			return RedirectToAction(nameof(Index));
		}
	}
}