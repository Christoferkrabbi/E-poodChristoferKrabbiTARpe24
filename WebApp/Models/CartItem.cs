using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class CartItem
	{

        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 1;
	}
}