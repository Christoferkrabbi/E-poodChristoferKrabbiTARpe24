using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities
{
	public class PlayTable
	{
		[Key]
		public string TableID { get; set; } = null!;

		public string Name { get; set; } = null!;
		public int Capacity { get; set; }
		public string? Location { get; set; }
	}
}