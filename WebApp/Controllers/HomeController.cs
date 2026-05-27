using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Linq;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IOrderService _orderService;

		public IActionResult Index()
		{
			return View();
		}

		public HomeController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<IActionResult> CreateOrder(int productId)
		{
			var orderDto = new OrderCreateDto
			{
				ProductId = productId,
				UserId = 1
			};

			var serviceResult = await _orderService.CreateOrderAsync(orderDto);

			string message = "Something went wrong";

			if (serviceResult.Message?.Contains("Payment failed") == true)
				message = "If this was real, you'd be furious rn, contacting support and stuff";
			else if (serviceResult.Message?.Contains("Order created") == true)
				message = "If this was real, your purcase would have been completed and youd be really happy or you'd probably regret spending your money. But either way... here you are!";

			var model = new OrderResultViewModel
			{
				Result = message,
				IsSuccess = serviceResult.IsSuccess
			};

			// Record completed order in in-memory storage so history can show purchased items and cost
			if (serviceResult.IsSuccess && serviceResult.Message?.Contains("Order created") == true)
			{
				// reconstruct purchased item (matches StoreController listing)
				var products = new List<Product>
				{
					new Product { Id = 1, Name = "Sword", Price = 50 },
					new Product { Id = 2, Name = "Shield", Price = 35 },
					new Product { Id = 3, Name = "Potion", Price = 10 }
				};

				var prod = products.FirstOrDefault(p => p.Id == productId);

				var items = new List<CartItem>();
				if (prod != null)
				{
					items.Add(new CartItem
					{
						ProductId = prod.Id,
						Name = prod.Name,
						Price = prod.Price,
						Quantity = 1
					});
				}
				else
				{
					items.Add(new CartItem
					{
						ProductId = productId,
						Name = "Unknown product",
						Price = 0,
						Quantity = 1
					});
				}

				var username = User?.Identity?.IsAuthenticated == true ? User.Identity.Name : "anonymous";

				var order = new Order
				{
					Items = items,
					Total = items.Sum(i => i.Price * i.Quantity),
					CreatedAt = DateTime.Now,
					Username = username
				};

				OrderStorage.Orders.Add(order);
			}

			return View("OrderResult", model);
		}

		[Authorize]
		public IActionResult OrderHistory()
		{
			var username = User?.Identity?.Name ?? "";
			var ordersForUser = OrderStorage.Orders
				.Where(o => string.Equals(o.Username, username, StringComparison.OrdinalIgnoreCase))
				.OrderByDescending(o => o.CreatedAt)
				.ToList();

			return View(ordersForUser);
		}
	}
}