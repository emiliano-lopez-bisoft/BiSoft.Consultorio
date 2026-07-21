using BiSoft.Consultorio.Api.DTOs.Configurations;

namespace BiSoft.Consultorio.Api.Extensions
{
    public static class ConfigurationExtension
    {
        public static GeneralConfigurations GetGeneralConfigurations(this IConfiguration configuration)
        {
            var seguridadConnection = configuration.GetConnectionString("Seguridad");
            var consultorioConnection = configuration.GetConnectionString("Consultorio");
            var rateLimiting = configuration.GetValue<int>("RateLimiting");
            var jwtConfig = configuration.GetJwtConfiguration();
            return new GeneralConfigurations(consultorioConnection, seguridadConnection, rateLimiting, jwtConfig);
        }

        private static string GetConnectionString(this IConfiguration configuration, string connectionName)
        {
            return configuration[$"DatabaseConnections:{connectionName}:ConnectionString"];
        }

        private static JwtConfiguration GetJwtConfiguration(this IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var secretKey = configuration["Jwt:SecretKey"];
            var authConfig = new JwtConfiguration(audience, issuer, secretKey);
            return authConfig;
        }
    }
}
