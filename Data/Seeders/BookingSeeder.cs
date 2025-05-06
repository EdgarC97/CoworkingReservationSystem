using CoworkingReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Data.Seeders
{
    public class BookingSeeder : ISeeder
    {
        private readonly AppDbContext _context;

        public BookingSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Bookings.AnyAsync())
            {
                // Data already exists, skip seeding
                return;
            }

            // Make sure we have users and rooms
            if (!await _context.Users.AnyAsync() || !await _context.Rooms.AnyAsync())
            {
                return;
            }

            var users = await _context.Users.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();

            // Create bookings for the next 7 days
            var bookings = new List<Booking>();
            var startDate = DateTime.UtcNow.Date;

            for (int day = 1; day <= 7; day++)
            {
                var currentDate = startDate.AddDays(day);

                // Morning booking (9 AM - 11 AM)
                bookings.Add(new Booking
                {
                    UserId = users[0].Id,
                    RoomId = rooms[0].Id,
                    StartTime = currentDate.AddHours(9),
                    EndTime = currentDate.AddHours(11),
                    Status = BookingStatus.Confirmed,
                    CancellationReason = "", // Add empty string instead of null
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

                // Afternoon booking (2 PM - 4 PM)
                bookings.Add(new Booking
                {
                    UserId = users[1].Id,
                    RoomId = rooms[1].Id,
                    StartTime = currentDate.AddHours(14),
                    EndTime = currentDate.AddHours(16),
                    Status = BookingStatus.Confirmed,
                    CancellationReason = "", // Add empty string instead of null
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

                // Evening booking (5 PM - 6 PM)
                bookings.Add(new Booking
                {
                    UserId = users[2].Id,
                    RoomId = rooms[2].Id,
                    StartTime = currentDate.AddHours(17),
                    EndTime = currentDate.AddHours(18),
                    Status = BookingStatus.Confirmed,
                    CancellationReason = "", // Add empty string instead of null
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            // Add a cancelled booking
            bookings.Add(new Booking
            {
                UserId = users[0].Id,
                RoomId = rooms[3].Id,
                StartTime = startDate.AddDays(2).AddHours(13),
                EndTime = startDate.AddDays(2).AddHours(15),
                Status = BookingStatus.Cancelled,
                CancellationReason = "Meeting rescheduled",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow
            });

            await _context.Bookings.AddRangeAsync(bookings);
            await _context.SaveChangesAsync();
        }
    }
}