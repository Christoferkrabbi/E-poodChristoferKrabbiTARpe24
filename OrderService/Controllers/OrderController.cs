using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateOrder()
        {
            var response = await _httpClient.PostAsync(
                "https://localhost:xxxx/api/payment/check", null);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Payment failed");

            return Ok("Order created");
        }
    }
}
