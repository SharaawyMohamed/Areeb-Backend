using Areeb.Application.Common.Responses.EventResponses;
using Areeb.Application.Features.Categories.Commands.CreateCategory;
using Areeb.Application.Features.Categories.Commands.UpdateCategory;
using Areeb.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.MappingProfiles
{
	public class CategoryMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<CreateCategoryCommand, Category>()
				.Ignore(dest => dest.ImageUrl);

			config.NewConfig<Category, CategoryResponse>();

			config.NewConfig<UpdateCategoryCommand, Category>()
				.Ignore(dest => dest.Id)
				.Ignore(dest => dest.ImageUrl);


		}
	}
}
