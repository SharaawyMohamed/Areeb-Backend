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

namespace Areeb.Application.Features.Categories.Commands.UpdateCategory
{
	public class UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateCategoryCommand> validator, IMediaService media) : IRequestHandler<UpdateCategoryCommand, Response>
	{
		private readonly IValidator<UpdateCategoryCommand> _validator = validator;
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IMediaService _media = media;

		public async Task<Response> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return await Response.ValidationFailureAsync(validationResult.Errors.ToList(), HttpStatusCode.UnprocessableEntity);
			}

			var category = await _unitOfWork.Repository<int,Category>().GetByIdAsync(request.Id);
			if (category == null)
			{
				return await Response.FailureAsync("Category not found", HttpStatusCode.NotFound);
			}

			var imageUrl = await _media.UploadImageAsync(request.Image);
			if (imageUrl == null)
			{
				return await Response.FailureAsync("Image upload failed", HttpStatusCode.InternalServerError);
			}
			await _media.DeleteAsync(category.ImageUrl);
			category.ImageUrl = imageUrl;
			category.Name=request.Name;

			await _unitOfWork.Repository<int,Category>().AddAsync(category);
			await _unitOfWork.SaveChangesAsync();

			var response = category.Adapt<CategoryResponse>();
			response.ImageUrl=imageUrl;
			return await Response.SuccessAsync(response, "Category updated successfully!");


		}
	}
}
