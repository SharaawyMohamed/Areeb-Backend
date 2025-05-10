using Areeb.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Domain.Interfaces
{
	public interface IAuthService
	{
		Task<string> CreateTokenAsync(User user, UserManager<User> _userManager);

	}
}
