using WebApp.Models;

namespace WebApp.Data
{
	public static class OrderStorage
	{
		public static List<Order> Orders { get; set; } = new();
	}
}