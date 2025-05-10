using Microsoft.AspNetCore.Identity;

namespace Areeb.Domain.Entities
{
	public class User:IdentityUser
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int Age { get; set; }
		public bool Gender { get; set; }
		public string? ProfilePictureUrl { get; set; } = string.Empty;
		public virtual ICollection<Booking> Bookings { get; set; }=new HashSet<Booking>();
	}
}
