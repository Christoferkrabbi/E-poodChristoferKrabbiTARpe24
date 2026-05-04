namespace WebApp.Models
{
	public class Order
	{
		public List<CartItem> Items { get; set; } = new List<CartItem>();
		public decimal Total { get; set; }
		public DateTime CreatedAt { get; set; }

		// owner of this order (local username)
		public string Username { get; set; } = string.Empty;
	}
}
