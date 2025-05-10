using Areeb.APIs.RequestObjects;
using Areeb.Application.Features.Categories.Commands.CreateCategory;
using Areeb.Application.Features.Categories.Commands.DeleteCategory;
using Areeb.Application.Features.Categories.Commands.UpdateCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Areeb.APIs.Controllers
{
	public class CategoryController(IMediator mediator) : BaseAPI
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost("create")]
		public async Task<ActionResult> CreateCategory([FromForm] CreateCategoryCommand command)
		{
			return Ok(await _mediator.Send(command));
		}

		[HttpPut("Update/{id}")]
		public async Task<ActionResult> UpdateCategory(int id, [FromForm] UpdateCategory request)
		{
			var command = new UpdateCategoryCommand(id, request.Name, request.image);
			return Ok(await _mediator.Send(command));
		}

		[HttpDelete("Delete/{id}")]
		public async Task<ActionResult> DeleteCategory(int id)
		{
			var command = new DeleteCategoryCommand(id);
			return Ok(await _mediator.Send(command));
		}

	}
}
