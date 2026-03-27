using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CreateOrder()
        {
            var response = await _httpClient.PostAsync(
                "https://localhost:xxxx/api/order/create", null);

            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Result = result;
            return View();
        }


    }
}
