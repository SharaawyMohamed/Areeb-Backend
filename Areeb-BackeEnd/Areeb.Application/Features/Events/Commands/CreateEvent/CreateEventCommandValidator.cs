using Areeb.Application.Common.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace Areeb.Application.Features.Events.Commands.CreateEvent
{
	public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
	{
		public CreateEventCommandValidator()
		{
			RuleFor(x => x.Title).NotEmpty().MaximumLength(100);

			RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);

			RuleFor(x => x.StartDate).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);

			RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate);

			RuleFor(x => x.Location).NotEmpty().MaximumLength(200);

			RuleFor(x => x.Capacity).NotEmpty().GreaterThan(0);

			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0);

			RuleFor(x => x.Image).NotNull().Must(BeAValidImage);
		}

		private bool BeAValidImage(IFormFile file)
		{
			if (file == null) return false;

			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			if (!allowedExtensions.Contains(extension))
				return false;

			var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
			if (!allowedMimeTypes.Contains(file.ContentType.ToLowerInvariant()))
				return false;

			if (file.Length > 5 * 1024 * 1024) 
				return false;

			return true;
		}
	}
}