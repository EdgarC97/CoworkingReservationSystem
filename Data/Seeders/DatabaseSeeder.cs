namespace CoworkingReservationSystem.Data.Seeders
{
    public class DatabaseSeeder
    {
        private readonly IEnumerable<ISeeder> _seeders;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(IEnumerable<ISeeder> seeders, ILogger<DatabaseSeeder> logger)
        {
            _seeders = seeders;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Starting database seeding...");

                foreach (var seeder in _seeders)
                {
                    _logger.LogInformation($"Running seeder: {seeder.GetType().Name}");
                    await seeder.SeedAsync();
                }

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