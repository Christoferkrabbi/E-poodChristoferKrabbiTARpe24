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
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(string address, string city, string postalCode)
    {
        var cart = GetCart();

        if (!cart.Any())
            return RedirectToAction("Index");

        // Grab the first item from the cart to pass to the existing CreateOrder logic
        var firstItem = cart.First();

        // Clear the user's active session cart basket out completely
        SaveCart(new List<CartItem>());

        // FIX: Instead of loading an empty success view, route directly to your real order creation service!
        return RedirectToAction("CreateOrder", "Home", new { productId = firstItem.ProductId });
    }

    [HttpGet]
    public IActionResult Success()
    {
        if (TempData["OrderId"] == null)
        {
            return RedirectToAction("Index", "Store");
        }

        ViewBag.OrderId = TempData["OrderId"];
        ViewBag.TotalAmount = TempData["TotalAmount"]?.ToString();

        // Utilizing your exact existing OrderResultViewModel properties
        var viewModel = new OrderResultViewModel
        {
            IsSuccess = true,
            Result = "Makse sooritatud edukalt (Payment Service OK)"
        };

        return View(viewModel);
    }


}