using Areeb.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Areeb.Persistence.Data
{
	public class AreebDbContext:IdentityDbContext<User>
	{
		public AreebDbContext(DbContextOptions<AreebDbContext> options):base(options)
		{
		}
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Category> Categories { get; set; }
	}
}
