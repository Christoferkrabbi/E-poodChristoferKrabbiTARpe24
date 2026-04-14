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

		var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

		if (existingItem != null)
		{
			existingItem.Quantity++;
		}
		else
		{
			cart.Add(new CartItem
			{
				ProductId = productId,
				Name = name,
				Price = price,
				Quantity = 1
			});
		}

		SaveCart(cart);

		return RedirectToAction("Index", "Store");
	}

	[HttpPost]
	public IActionResult RemoveFromCart(int productId)
	{
		var cart = GetCart();

		var item = cart.FirstOrDefault(x => x.ProductId == productId);
		if (item != null)
		{
			cart.Remove(item);
		}

		SaveCart(cart);

		return RedirectToAction("Index");
	}

	[HttpPost]
	public IActionResult Checkout()
	{
		var cart = GetCart();

		if (!cart.Any())
			return RedirectToAction("Index");

		// For now: send first item (simple version)
		// BUT this is where you'd normally send full cart to OrderService

		var firstItem = cart.First();

		// clear cart after checkout
		SaveCart(new List<CartItem>());

		return RedirectToAction("CreateOrder", "Home", new { productId = firstItem.ProductId });
	}
}