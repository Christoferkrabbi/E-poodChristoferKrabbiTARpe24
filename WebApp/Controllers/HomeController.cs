using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Linq;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{

	public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public IActionResult Index()
        {
            return View();
        }

        public HomeController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }
        public async Task<IActionResult> CreateOrder(int productId)
        {
            var orderData = new
            {
                ProductId = productId,
                UserId = 1
            };

            var content = new StringContent(
                JsonSerializer.Serialize(orderData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                "http://localhost:5047/api/order/create",
                content
            );

			var json = await response.Content.ReadAsStringAsync();

			string message = "Something went wrong";

			if (json.Contains("Payment failed"))
				message = "If this was real, you'd be furious rn, contacting support and stuff";
			else if (json.Contains("Order created"))
				message = "If this was real, your purcase would have been completed and youd be really happy or you'd probably regret spending your money. But either way... here you are!";

			var model = new OrderResultViewModel
			{
				Result = message,
				IsSuccess = response.IsSuccessStatusCode
			};

			// Record completed order in in-memory storage so history can show purchased items and cost
			if (response.IsSuccessStatusCode && json.Contains("Order created"))
			{
				// simple product lookup to reconstruct purchased item (matches StoreController listing)
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

				var order = new Order
				{
					Items = items,
					Total = items.Sum(i => i.Price * i.Quantity),
					CreatedAt = DateTime.Now
				};

				OrderStorage.Orders.Add(order);
			}
			
			return View("OrderResult", model);
        }

		public IActionResult OrderHistory()
		{
			return View(OrderStorage.Orders);
		}
	}
}
