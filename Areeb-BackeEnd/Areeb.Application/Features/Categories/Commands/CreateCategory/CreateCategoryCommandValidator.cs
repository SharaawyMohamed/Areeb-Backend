using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Categories.Commands.CreateCategory
{
	public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			
		}
	}
}
