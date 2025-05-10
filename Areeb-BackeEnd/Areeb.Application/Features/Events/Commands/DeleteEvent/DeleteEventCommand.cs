using Areeb.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Events.Commands.DeleteEvent
{
	public record DeleteEventCommand(int Id):IRequest<Response>;
}
