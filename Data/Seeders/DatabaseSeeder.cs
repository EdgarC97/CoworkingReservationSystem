namespace CoworkingReservationSystem.Data.Seeders
{
    public class DatabaseSeeder
    {
        private readonly IEnumerable<ISeeder> _seeders;
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly UserSeeder _userSeeder;
        private readonly RoomSeeder _roomSeeder;
        private readonly BookingSeeder _bookingSeeder;

        public DatabaseSeeder(
            IEnumerable<ISeeder> seeders,
            ILogger<DatabaseSeeder> logger,
            UserSeeder userSeeder,
            RoomSeeder roomSeeder,
            BookingSeeder bookingSeeder)
        {
            _seeders = seeders;
            _logger = logger;
            _userSeeder = userSeeder;
            _roomSeeder = roomSeeder;
            _bookingSeeder = bookingSeeder;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Starting database seeding...");

                // Run seeders in specific order
                _logger.LogInformation("Seeding users...");
                await _userSeeder.SeedAsync();

                _logger.LogInformation("Seeding rooms...");
                await _roomSeeder.SeedAsync();

                _logger.LogInformation("Seeding bookings...");
                await _bookingSeeder.SeedAsync();

                _logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
    }
}