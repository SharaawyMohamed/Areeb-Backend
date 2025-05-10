using Areeb.Application.Common.Responses;
using Areeb.Domain.Entities;
using Areeb.Domain.Interfaces;
using Areeb.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Events.Commands.DeleteEvent
{
	public class DeleteEventCommandHandler(IUnitOfWork unitOfWork, IMediaService media) : IRequestHandler<DeleteEventCommand, Response>
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IMediaService _media = media;
		public async Task<Response> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
		{
			var eventObject = await _unitOfWork.Repository<int, Event>().GetByIdAsync(request.Id);
			if (eventObject == null)
			{
				return await Response.FailureAsync("Event not found", HttpStatusCode.NotFound);
			}

			if (eventObject.Bookings != null && eventObject.Bookings.Count > 0)
			{
				return await Response.FailureAsync("Event cannot be deleted as it has bookings", HttpStatusCode.BadRequest);
			}

			if (eventObject.ImageUrl != null)
			{
				await _media.DeleteAsync(eventObject.ImageUrl);
			}

			_unitOfWork.Repository<int, Event>().Remove(eventObject);
			await _unitOfWork.SaveChangesAsync();

			return await Response.SuccessAsync("Event deleted successfully");
		}
	}
}
