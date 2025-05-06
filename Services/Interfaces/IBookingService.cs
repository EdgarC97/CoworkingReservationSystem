using CoworkingReservationSystem.DTOs.Requests;
using CoworkingReservationSystem.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoworkingReservationSystem.Interfaces.Services
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request, int userId);
        Task<BookingResponse> CancelBookingAsync(int bookingId, CancelBookingRequest request, int userId);
        Task<IEnumerable<BookingResponse>> GetUserBookingsAsync(int userId);
        Task<BookingResponse> GetBookingByIdAsync(int id);
    }
}