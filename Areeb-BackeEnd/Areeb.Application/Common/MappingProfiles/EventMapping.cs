using Areeb.Application.Common.Responses.EventResponses;
using Areeb.Application.Features.Events.Commands.CreateEvent;
using Areeb.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.MappingProfiles
{
	public class EventMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Event, CreateEventResponse>()
				.Ignore(dest => dest.AvailableToBook);

			config.NewConfig<CreateEventCommand, Event>()
				.Ignore(dest => dest.Id)
				.Ignore(dest => dest.ImageUrl);


		}
	}
}
