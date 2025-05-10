using Areeb.Application.Common.Responses;
using Areeb.Domain.Entities;
using Areeb.Domain.Interfaces;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Authentication.Commands.Login
{
	public class LoginCommandHandler(UserManager<User> userManager,
		SignInManager<User> singInManager,
		IAuthService authService,
		IValidator<LoginCommand> validator
		) : IRequestHandler<LoginCommand, Response>
	{
		private readonly UserManager<User> _userManager = userManager;
		private readonly SignInManager<User> _singInManager = singInManager;
		private readonly IAuthService _authService = authService;
		private readonly IValidator<LoginCommand> _validator = validator;
		public async Task<Response> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
			}

			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				return await Response.FailureAsync("Email not found!");
			}

			var result = await _singInManager.PasswordSignInAsync(user, request.Password, true, true);
			if (!result.Succeeded)
			{
				return await Response.FailureAsync("Invalid `Password`", HttpStatusCode.BadRequest);
			}


			var Token = await _authService.CreateTokenAsync(user, _userManager);
			if (Token == null)
			{
				return await Response.FailureAsync("Token generation failed", HttpStatusCode.InternalServerError);
			}

			var response = user.Adapt<LoginResponse>();
			response.Token = Token;

			return await Response.SuccessAsync(response, "Logged in successfully!");
		}
	}

}
