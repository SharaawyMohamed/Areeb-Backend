using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Features.Categories.Commands.UpdateCategory
{
	public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(x => x.Id).GreaterThan(0);

			RuleFor(x => x.Name).MaximumLength(100).Matches(@"^[a-zA-Z0-9\s\-]+$").When(x => !string.IsNullOrEmpty(x.Name));

			RuleFor(x => x.Image).Must(BeAValidImage).When(x => x.Image != null).Must(BeUnder5MB).When(x => x.Image != null);
		}

		private bool BeAValidImage(IFormFile file)
		{
			if (file == null) return true; 
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			return allowedExtensions.Contains(extension);
		}

		private bool BeUnder5MB(IFormFile file)
		{
			if (file == null) return true; 
			return file.Length <= 5 * 1024 * 1024; 
		}
	
	}
}
