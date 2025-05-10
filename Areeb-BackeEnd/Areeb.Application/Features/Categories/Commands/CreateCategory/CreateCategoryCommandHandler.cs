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

namespace Areeb.Application.Features.Categories.Commands.CreateCategory
{
	public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateCategoryCommand> validator,IMediaService media) : IRequestHandler<CreateCategoryCommand, Response>
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IValidator<CreateCategoryCommand> _validator = validator;
		private readonly IMediaService _media = media;
		public async Task<Response> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var validationResult = _validator.Validate(request);
			if (!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
			}

			var imageUrl = await _media.UploadImageAsync(request.Image);
			if(imageUrl == null)
			{
				return await Response.FailureAsync("Image upload failed", HttpStatusCode.InternalServerError);
			}

			var category = request.Adapt<Category>();
			category.ImageUrl = imageUrl;

			await _unitOfWork.Repository<int, Category>().AddAsync(category);
			await _unitOfWork.SaveChangesAsync();

			return await Response.SuccessAsync(category.Adapt<CategoryResponse>(), "Category created successfully!");
		}
	}

}
