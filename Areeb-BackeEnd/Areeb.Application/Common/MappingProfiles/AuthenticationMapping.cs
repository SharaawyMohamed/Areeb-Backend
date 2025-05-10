using Areeb.Application.Common.Responses;
using Areeb.Application.Features.Authentication.Commands.Login;
using Areeb.Application.Features.Authentication.Commands.Register;
using Areeb.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.MappingProfiles
{
	public class AuthenticationMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<User, LoginResponse>();

			config.NewConfig<RegisterCommand, User>();
		}
	}
}
