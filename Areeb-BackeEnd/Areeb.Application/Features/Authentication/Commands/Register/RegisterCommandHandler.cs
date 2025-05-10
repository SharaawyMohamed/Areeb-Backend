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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Areeb.Application.Features.Authentication.Commands.Register
{
	public class RegisterCommandHandler(UserManager<User> userManager,
		IAuthService authService,
		IValidator<RegisterCommand> validator
		) : IRequestHandler<RegisterCommand, Response>
	{
		private readonly UserManager<User> _userManager = userManager;
		private readonly IAuthService _authService = authService;
		private readonly IValidator<RegisterCommand> _validator = validator;

		public async Task<Response> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
			}

			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user != null)
			{
				return await Response.FailureAsync("Email already Exist!", HttpStatusCode.BadRequest);
			}

			var account = request.Adapt<User>();
			account.UserName = request.Email.Split('@')[0];

			await _userManager.CreateAsync(account, request.Password);

			var Token = await _authService.CreateTokenAsync(account, _userManager);
			if (Token == null)
			{
				return await Response.FailureAsync("Token not generated!", HttpStatusCode.BadRequest);
			}

			var response = account.Adapt<RegisterResponse>();
			response.Token = Token;

			return await Response.SuccessAsync(response, "Registration Successfully.");
		}
	}
}
