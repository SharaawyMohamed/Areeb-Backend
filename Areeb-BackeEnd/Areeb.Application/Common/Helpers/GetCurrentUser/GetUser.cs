using Areeb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.Helpers.GetCurrentUser
{
	internal static class GetUser
	{
		public static async Task<User> GetCurrentUserAsync(IHttpContextAccessor _contextAccessor, UserManager<User> _userManager)
		{
			var userClaims = _contextAccessor.HttpContext?.User;

			if (userClaims == null || !userClaims.Identity!.IsAuthenticated)
			{
				return null!;
			}

			var userEmail = userClaims.FindFirstValue(ClaimTypes.Email);

			if (string.IsNullOrEmpty(userEmail))
			{
				return null!;
			}
			return (await _userManager.FindByEmailAsync(userEmail))!;
		}
	}
}
