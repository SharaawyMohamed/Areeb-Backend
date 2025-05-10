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

namespace Areeb.Application.Features.Categories.Commands.DeleteCategory
{
	public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMediaService media) : IRequestHandler<DeleteCategoryCommand, Response>
	{
		private readonly IUnitOfWork _unitOfWork = unitOfWork;
		private readonly IMediaService _media = media;
		public async Task<Response> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await _unitOfWork.Repository<int, Category>().GetByIdAsync(request.Id);
			if (category == null)
			{
				return await Response.FailureAsync("Category not found!");
			}
			if (category.Events != null && category.Events.Count > 0)
			{
				return await Response.FailureAsync("Category cannot be deleted as it is associated with events!", HttpStatusCode.Conflict);
			}
			if (category.ImageUrl != null)
			{
				await _media.DeleteAsync(category.ImageUrl);
			}
			 _unitOfWork.Repository<int, Category>().Remove(category);
			await _unitOfWork.SaveChangesAsync();

			return await Response.SuccessAsync("Category deleted successfully!");
		}
	}
}
