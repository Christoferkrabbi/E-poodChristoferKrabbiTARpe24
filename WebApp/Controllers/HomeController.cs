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

       [HttpPost]
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
				"https://localhost:7297/api/order/create", content);

			var result = await response.Content.ReadAsStringAsync();

			ViewBag.Result = result;

			return View("OrderResult");
		}
	}
}
