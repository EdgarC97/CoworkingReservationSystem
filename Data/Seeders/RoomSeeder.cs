using CoworkingReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Data.Seeders
{
    public class RoomSeeder : ISeeder
    {
        private readonly AppDbContext _context;

        public RoomSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Rooms.AnyAsync())
            {
                // Data already exists, skip seeding
                return;
            }

            var rooms = new List<Room>
            {
                new Room
                {
                    Name = "Conference Room A",
                    Description = "Large conference room with projector and whiteboard",
                    Capacity = 20,
                    HasProjector = true,
                    HasWhiteboard = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Name = "Meeting Room B",
                    Description = "Medium-sized meeting room with whiteboard",
                    Capacity = 10,
                    HasProjector = false,
                    HasWhiteboard = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Name = "Presentation Room C",
                    Description = "Small presentation room with projector",
                    Capacity = 8,
                    HasProjector = true,
                    HasWhiteboard = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Name = "Brainstorming Room D",
                    Description = "Creative space with whiteboards on all walls",
                    Capacity = 6,
                    HasProjector = false,
                    HasWhiteboard = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Room
                {
                    Name = "Executive Room E",
                    Description = "Executive meeting room with high-end equipment",
                    Capacity = 12,
                    HasProjector = true,
                    HasWhiteboard = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await _context.Rooms.AddRangeAsync(rooms);
            await _context.SaveChangesAsync();
        }
    }
}