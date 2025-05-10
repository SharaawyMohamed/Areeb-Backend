using Areeb.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Authentication.Commands.Register
{
	public record RegisterCommand(
		string FirstName,
		string LastName,
		string Email,
		string Password,
		string ConfirmPassword,
		string PhoneNumber,
		string Address,
		bool Gender) : IRequest<Response>;
	
	}
