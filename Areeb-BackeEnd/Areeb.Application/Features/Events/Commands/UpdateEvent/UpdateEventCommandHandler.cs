using Areeb.Application.Common.Responses;
using Areeb.Application.Common.Responses.EventResponses;
using Areeb.Domain.Entities;
using Areeb.Domain.Interfaces;
using Areeb.Domain.Repositories;
using FluentValidation;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Events.Commands.UpdateEvent
{
	public class UpdateEventCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateEventCommand> validator, IMediaService media) : IRequestHandler<UpdateEventCommand, Response>
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidator<UpdateEventCommand> _validator = validator;
		private readonly IMediaService _media = media;
		public async Task<Response> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
			}

			var eventObject = await _unitOfWork.Repository<int, Event>().GetByIdAsync(request.Id);
			if (eventObject == null)
			{
				return await Response.FailureAsync("Event not found", HttpStatusCode.NotFound);
			}

			var eventImageUrl = await _media.UploadImageAsync(request.Image);
			if (eventImageUrl == null)
			{
				return await Response.FailureAsync("Image upload failed", HttpStatusCode.InternalServerError);
			}

			if (eventObject.ImageUrl != null)
			{
				await _media.DeleteAsync(eventObject.ImageUrl);
			}

			eventObject.ImageUrl = eventImageUrl;
			eventObject.Title = request.Title;
			eventObject.Price = request.Price;
			eventObject.StartDate = request.StartDate;
			eventObject.EndDate = request.EndDate;
			eventObject.Location = request.Location;
			eventObject.Description = request.Description;
			eventObject.Capacity = request.Capacity;
			_unitOfWork.Repository<int, Event>().Update(eventObject);
			await _unitOfWork.SaveChangesAsync();

			return await Response.SuccessAsync(eventObject.Adapt<CreateEventResponse>(), "Event updated successfully!");
		}
	}
}
