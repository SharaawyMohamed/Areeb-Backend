using Areeb.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Categories.Commands.CreateCategory
{
	public record CreateCategoryCommand(string Name,IFormFile Image) : IRequest<Response>;
	
}
