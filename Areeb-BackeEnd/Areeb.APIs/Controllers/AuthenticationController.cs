using Areeb.Application.Features.Authentication.Commands.Login;
using Areeb.Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.APIs.Controllers
{
	public class AuthenticationController(IMediator mediatR) :BaseAPI
	{
		private readonly IMediator _mediatR = mediatR;

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginCommand command)
		{
			return Ok(await _mediatR.Send(command));
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterCommand command)
		{
			return Ok(await _mediatR.Send(command));
		}


	}
}
