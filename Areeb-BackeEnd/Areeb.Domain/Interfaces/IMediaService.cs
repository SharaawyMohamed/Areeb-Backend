using Microsoft.AspNetCore.Http;

namespace Areeb.Domain.Interfaces
{
	public interface IMediaService
	{
		Task<string> UploadImageAsync(IFormFile? media);

		Task DeleteAsync(string url);
	}
}
