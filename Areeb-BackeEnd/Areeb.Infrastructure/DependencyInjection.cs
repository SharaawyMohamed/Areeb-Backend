using Areeb.Domain.Interfaces;
using Areeb.Infrastructure.Authentication;
using Areeb.Infrastructure.ExternalServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{

			#region JWT Configurations
			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					//options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				 .AddJwtBearer(o =>
				 {
					 o.TokenValidationParameters = new TokenValidationParameters
					 {
						 ValidateIssuerSigningKey = true,
						 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]!)),

						 ValidateIssuer = true,
						 ValidIssuer = configuration["Token:Issuer"],

						 ValidateAudience = true,
						 ValidAudience = configuration["Token:Audience"],

						 ValidateLifetime = true,
						 ClockSkew = TimeSpan.Zero, // To Strict validation of token expiration

					 };
				 });
			#endregion

			#region CORS Configurations
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();

				});
			});
			#endregion

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IMediaService, MediaService>();

			return services;
		}
	}
}
