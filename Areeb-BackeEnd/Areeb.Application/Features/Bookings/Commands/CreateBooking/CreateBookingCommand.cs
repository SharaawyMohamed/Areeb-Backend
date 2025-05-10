using Areeb.Application.Common.Responses;
using MediatR;

namespace Areeb.Application.Features.Bookings.Commands.CreateBooking
{
	public record CreateBookingCommand(
		int EventId,
		int UserId,
		int NumberOfTickets,
		decimal TotalAmount) : IRequest<Response>;
}
