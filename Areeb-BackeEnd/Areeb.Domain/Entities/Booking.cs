using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Domain.Entities
{
	public class Booking:BaseEntity<int>
	{
		public DateTime BookingDate { get; set; }
		public decimal TotalAmount { get; set; }

		public int EventId { get; set; }
		public virtual Event Event { get; set; }

		public int AttendeeId { get; set; }
		public virtual User Attendee { get; set; }
	}
}
