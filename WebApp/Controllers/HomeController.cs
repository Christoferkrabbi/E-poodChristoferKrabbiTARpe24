using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
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

			return View("OrderResult", model);
        }
    }
}
