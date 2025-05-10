using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Domain.Entities
{
	public class Event : BaseEntity<int>
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Location { get; set; } = string.Empty;
		public int Capacity { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public bool AvailableToBook { get; set; }
		
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }

		public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
	}
	
}
