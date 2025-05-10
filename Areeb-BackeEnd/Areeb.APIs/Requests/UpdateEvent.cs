namespace Areeb.APIs.Requests
{
	public record UpdateEvent(string Title,
		string Description,
		DateTime StartDate,
		DateTime EndDate,
		string Location,
		int Capacity,
		decimal Price,
		IFormFile Image);
}
