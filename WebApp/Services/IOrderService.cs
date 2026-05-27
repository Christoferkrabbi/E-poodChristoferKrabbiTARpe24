using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
	public interface IOrderService
	{
		Task<OrderServiceResultDto> CreateOrderAsync(OrderCreateDto order);
	}
}