using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Domain.Entities
{
	public class Category:BaseEntity<int>
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public virtual ICollection<Event> Events { get; set; } = new List<Event>();
	}
}
