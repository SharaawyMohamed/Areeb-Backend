﻿using Areeb.Domain.Entities;
using Areeb.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Areeb.Infrastructure.Authentication
{
	public class AuthService : IAuthService
	{

		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> CreateTokenAsync(User user, UserManager<User> _userManager)
		{
			var userClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Name,$"{user.FirstName}{user.LastName}"),
				new Claim(ClaimTypes.Email,user.Email!),
			};

			var Roles = await _userManager.GetRolesAsync(user);
			foreach (var role in Roles)
			{
				userClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			var authKeyInByets = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));

			var JwtObject = new JwtSecurityToken(
				issuer: _configuration["Token:Issuer"],
				audience: _configuration["Token:Audience"],
				claims: userClaims,
				expires: DateTime.Now.AddDays(double.Parse(_configuration["Token:ExpiryDays"]!)),
				signingCredentials: new SigningCredentials(authKeyInByets, SecurityAlgorithms.HmacSha256)
			);
			return new JwtSecurityTokenHandler().WriteToken(JwtObject);
		}
	}
}

