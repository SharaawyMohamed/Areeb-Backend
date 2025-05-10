using Areeb.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Events.Commands.CreateEvent
{
	public record CreateEventCommand(
		string Title,
		string Description,
		DateTime StartDate,
		DateTime EndDate,
		string Location,
		int Capacity,
		decimal Price,
		IFormFile Image,
		bool AvailableToBook) : IRequest<Response>;

}
