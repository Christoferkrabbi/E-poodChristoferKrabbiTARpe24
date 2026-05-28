using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Entities
{
	public class PlayTable
	{
		[Key]
		public Guid Id { get; set; }
		public string TableName { get; set; }

		public string LocationStoreName { get; set; }

		public string TableDescription { get; set; }

		[NotMapped]
		public List<string> BookingIDs { get; set; } = new();

		public DateTime CreatedAt { get; set; }

		public DateTime ModifiedAt { get; set; }

		public DateTime LastVisitAt { get; set; }
		//[Key]
		//public string TableID { get; set; } = null!;
		//public string Name { get; set; } = null!;
		//public int Capacity { get; set; }
		//public string? Location { get; set; }
	}

}
