using Areeb.Domain.Entities;
using Areeb.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Areeb.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AreebDbContext>(options =>
				options.UseLazyLoadingProxies()
				.UseSqlServer(configuration.GetConnectionString("DbConnection")));


			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AreebDbContext>()
				.AddDefaultTokenProviders();

			return services;
		}
	}
}
