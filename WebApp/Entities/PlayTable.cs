using System.ComponentModel.DataAnnotations;

public class PlayTable
{
	[Key]
	public Guid Id { get; set; }

	public string TableName { get; set; }

	public string LocationStoreName { get; set; }

	public string TableDescription { get; set; }

	public List<string> BookingIDs { get; set; } = new();

	public DateTime CreatedAt { get; set; }

	public DateTime ModifiedAt { get; set; }

	public DateTime LastVisitAt { get; set; }
}