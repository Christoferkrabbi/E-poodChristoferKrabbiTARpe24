using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory factory)
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

			var resultString = await response.Content.ReadAsStringAsync();

			var model = new OrderResultViewModel
			{
				Result = resultString ?? "No result",
				IsSuccess = resultString?.Contains("success") ?? false
			};

			return View("OrderResult", model);
		}
	}
}
