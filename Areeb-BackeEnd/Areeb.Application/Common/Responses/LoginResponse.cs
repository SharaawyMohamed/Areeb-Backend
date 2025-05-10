using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.Responses
{
	public class LoginResponse
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public bool Gender { get; set; }
		public int Age { get; set; }
		public string ProfilePictureUrl { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
	}
}
