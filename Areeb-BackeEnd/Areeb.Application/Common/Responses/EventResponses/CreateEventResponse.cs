using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.Responses.EventResponses
{
	public class CreateEventResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Location { get; set; }
		public int Capacity { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public bool AvailableToBook { get; set; }
	}

}
