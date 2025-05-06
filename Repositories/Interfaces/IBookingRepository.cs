using CoworkingReservationSystem.Models;

namespace CoworkingReservationSystem.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetByRoomIdAsync(int roomId);
        Task<IEnumerable<Booking>> GetOverlappingBookingsAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeBookingId = null);
        Task<Booking> CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}