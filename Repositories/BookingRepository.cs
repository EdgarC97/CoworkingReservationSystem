using CoworkingReservationSystem.Data;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByRoomIdAsync(int roomId)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Where(b => b.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetOverlappingBookingsAsync(int roomId, DateTime startTime, DateTime endTime, int? excludeBookingId = null)
        {
            var query = _context.Bookings
                .Where(b => b.RoomId == roomId &&
                           b.Status != BookingStatus.Cancelled &&
                           ((b.StartTime <= startTime && b.EndTime > startTime) ||
                            (b.StartTime < endTime && b.EndTime >= endTime) ||
                            (b.StartTime >= startTime && b.EndTime <= endTime)));

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}