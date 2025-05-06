using CoworkingReservationSystem.Data;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime)
        {
            var allRooms = await _context.Rooms.ToListAsync();
            var unavailableRoomIds = await _context.Bookings
                .Where(b => b.Status != BookingStatus.Cancelled &&
                           ((b.StartTime <= startTime && b.EndTime > startTime) ||
                            (b.StartTime < endTime && b.EndTime >= endTime) ||
                            (b.StartTime >= startTime && b.EndTime <= endTime)))
                .Select(b => b.RoomId)
                .Distinct()
                .ToListAsync();

            return allRooms.Where(r => !unavailableRoomIds.Contains(r.Id));
        }

        public async Task<Room> CreateAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
