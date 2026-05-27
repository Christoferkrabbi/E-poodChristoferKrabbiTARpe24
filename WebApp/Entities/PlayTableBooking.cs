using System.ComponentModel.DataAnnotations;

public class PlayTableBooking
{
	[Key]
	public int BookingID { get; set; }

	public string TableID { get; set; }

	public string UserID { get; set; }

	public DateTime FromTime { get; set; }

	public DateTime ToTime { get; set; }

	public string BookingInfo { get; set; }
}