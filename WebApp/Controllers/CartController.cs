using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models;

public class CartController : Controller
{
	private const string CartKey = "Cart";

	private List<CartItem> GetCart()
	{
		var cartJson = HttpContext.Session.GetString(CartKey);
		return cartJson == null
			? new List<CartItem>()
			: JsonSerializer.Deserialize<List<CartItem>>(cartJson);
	}

	private void SaveCart(List<CartItem> cart)
	{
		HttpContext.Session.SetString(CartKey, JsonSerializer.Serialize(cart));
	}

	public IActionResult Index()
	{
		var cart = GetCart();
		return View(cart);
	}

	[HttpPost]
	public IActionResult AddToCart(int productId, string name, decimal price)
	{
		var cart = GetCart();

		cart.Add(new CartItem
		{
			ProductId = productId,
			Name = name,
			Price = price
		});

		SaveCart(cart);

		return RedirectToAction("Index", "Store");
	}

	[HttpPost]
	public IActionResult Checkout()
	{
		var cart = GetCart();

		if (!cart.Any())
			return RedirectToAction("Index");

		var firstItem = cart.First();

		return RedirectToAction("CreateOrder", "Home", new { productId = firstItem.ProductId });
	}
}