using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class Order
	{
        [Key]
		public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
		public decimal Total { get; set; }
		public DateTime CreatedAt { get; set; }

		// owner of this order (local username)
		public string UserName { get; set; } = string.Empty;
		public string UserEmail { get; set; }
	}
}
