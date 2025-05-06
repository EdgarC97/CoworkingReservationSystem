using System;

namespace CoworkingReservationSystem.Utils
{
    public static class EnvironmentHelper
    {
        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name) ??
                   throw new InvalidOperationException($"Environment variable '{name}' not found");
        }

        public static string GetConnectionString()
        {
            return GetEnvironmentVariable("DATABASE_CONNECTION");
        }

        public static string GetJwtSecret()
        {
            return GetEnvironmentVariable("JWT_SECRET");
        }

        public static string GetJwtIssuer()
        {
            return GetEnvironmentVariable("JWT_ISSUER");
        }

        public static string GetJwtAudience()
        {
            return GetEnvironmentVariable("JWT_AUDIENCE");
        }

        public static int GetJwtExpiryMinutes()
        {
            string value = GetEnvironmentVariable("JWT_EXPIRY_MINUTES");
            return int.TryParse(value, out int result) ? result : 60;
        }
    }
}