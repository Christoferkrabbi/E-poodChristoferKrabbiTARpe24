using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Entities
{
	public class PlayTableBooking
	{
		[Key]
		public int BookingID { get; set; }

		// 1. Ütle, et see on välisvõti, mis viitab PlayTable objektile
		[ForeignKey(nameof(PlayTable))]
		public Guid PlayTableID { get; set; }

		public string UserID { get; set; }

		public DateTime FromTime { get; set; }

		public DateTime ToTime { get; set; }

		public string BookingInfo { get; set; }

		// 2. Navigeerimisomadus
		public virtual PlayTable PlayTable { get; set; }
	}
}