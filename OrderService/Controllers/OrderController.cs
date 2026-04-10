using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var response = await _httpClient.PostAsync(
                "http://localhost:5109/api/payment/check",
                null
            );

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { success = false, message = "Payment failed" });
            }

            return Ok(new { success = true, message = "Order created successfully" });
        }
    }
}
