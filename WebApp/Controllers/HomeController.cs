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

            var resultString = await response.Content.ReadAsStringAsync();

            var model = new OrderResultViewModel
            {
                Result = resultString,
                IsSuccess = response.IsSuccessStatusCode
            };

            return View("OrderResult", model);
        }
    }
}
