using Areeb.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Categories.Commands.DeleteCategory
{
	public record DeleteCategoryCommand(int Id) : IRequest<Response>;
}
