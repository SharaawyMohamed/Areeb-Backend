using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Authentication.Commands.Register
{
	public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
	{
		public RegisterCommandValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);

			RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

			RuleFor(x => x.Email).NotEmpty().EmailAddress();

			RuleFor(x => x.Password).NotEmpty()
						   .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$")
						   .WithMessage("Password must contain at least one uppercase, one lowercase, one digit, and one special character (@$!%*?&#)");

			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

			RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\+?[0-9]{10,15}$");

			RuleFor(x => x.Address).NotEmpty().MaximumLength(250);
		}
	}
}
