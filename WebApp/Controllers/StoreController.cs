using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

public class StoreController : Controller
{
	public IActionResult Index()
	{
		var products = new List<Product>
	{
		new Product { Id = 1, Name = "Sword", Price = 50 },
		new Product { Id = 2, Name = "Shield", Price = 35 },
		new Product { Id = 3, Name = "Potion", Price = 10 }
	};

		return View(products);
	}
}