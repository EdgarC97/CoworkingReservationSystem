using CoworkingReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingReservationSystem.Data.Seeders
{
    public class UserSeeder : ISeeder
    {
        private readonly AppDbContext _context;

        public UserSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Users.AnyAsync())
            {
                // Data already exists, skip seeding
                return;
            }

            var users = new List<User>
            {
                new User
                {
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    FirstName = "Admin",
                    LastName = "User",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Email = "john@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("John123!"),
                    FirstName = "John",
                    LastName = "Doe",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Email = "jane@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Jane123!"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}