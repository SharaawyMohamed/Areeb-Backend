using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Authentication.Commands.Login
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty()
	           .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$")
	           .WithMessage("Password must contain at least one uppercase, one lowercase, one digit, and one special character (@$!%*?&#)");

		}
	}
}
