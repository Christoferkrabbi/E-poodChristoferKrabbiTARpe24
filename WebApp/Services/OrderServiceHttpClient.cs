using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
	public class OrderServiceHttpClient : IOrderService
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		public OrderServiceHttpClient(HttpClient client)
		{
			_client = client;
		}

		public async Task<OrderServiceResultDto> CreateOrderAsync(OrderCreateDto order)
		{
			var content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

			// POST to relative path; BaseAddress must be configured when registering the client
			var resp = await _client.PostAsync("api/order/create", content);
			var respText = await resp.Content.ReadAsStringAsync();

			return new OrderServiceResultDto
			{
				IsSuccess = resp.IsSuccessStatusCode,
				Message = respText
			};
		}
	}
}