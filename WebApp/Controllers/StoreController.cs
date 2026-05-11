using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

public class StoreController : Controller
{
	public IActionResult Index(string search)
	{
		var products = ProductStore.GetAll().ToList();

		if (!string.IsNullOrEmpty(search))
		{
			products = products
				.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}

		return View(products);
	}
}