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

namespace Areeb.Application.Features.Events.Commands.CreateEvent
{
	public class CreateEventCommandHandler(IUnitOfWork unitOfWork,IValidator<CreateEventCommand> validator,IMediaService media) : IRequestHandler<CreateEventCommand, Response>
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidator<CreateEventCommand> _validator=validator;
		private readonly IMediaService _media = media;

		public async Task<Response> Handle(CreateEventCommand request, CancellationToken cancellationToken)
		{
			var validationResult=_validator.Validate(request);
			if(!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(),HttpStatusCode.UnprocessableEntity);
			}

			var eventImageUrl = await _media.UploadImageAsync(request.Image);
			if(eventImageUrl == null)
			{
				return await Response.FailureAsync("Image upload failed", HttpStatusCode.InternalServerError);
			}

			var eventObject=request.Adapt<Event>();
			eventObject.ImageUrl=eventImageUrl; 

			await _unitOfWork.Repository<int, Event>().AddAsync(eventObject);
			await _unitOfWork.SaveChangesAsync();

			var response= eventObject.Adapt<CreateEventResponse>();
			response.AvailableToBook = true;
			return await Response.SuccessAsync(response , "Event created successfully!");
		}
	}
	
}
