using Areeb.APIs.Requests;
using Areeb.Application.Features.Events.Commands.CreateEvent;
using Areeb.Application.Features.Events.Commands.DeleteEvent;
using Areeb.Application.Features.Events.Commands.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.APIs.Controllers
{
	public class EventController(IMediator mediator):BaseAPI
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost("create")]
		public async Task<ActionResult> CreateEvent([FromForm] CreateEventCommand command)
		{
			return Ok(await _mediator.Send(command));
		}

		[HttpDelete("delete/{id}")]
		public async Task<ActionResult> DeleteEvent(int id)
		{
			return Ok(await _mediator.Send(new DeleteEventCommand(id)));
		}

		[HttpPut("update/{id}")]
		public async Task<ActionResult> UpdateEvent(int id, [FromForm] UpdateEvent request)
		{
			var command=new UpdateEventCommand(id,request.Title,request.Description, request.StartDate, request.EndDate,request.Location,request.Capacity,request.Price, request.Image);
			return Ok(await _mediator.Send(command));
		}


	}
}
