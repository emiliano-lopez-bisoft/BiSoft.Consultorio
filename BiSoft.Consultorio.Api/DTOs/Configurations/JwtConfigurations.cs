namespace BiSoft.Consultorio.Api.DTOs.Configurations
{
        public record JwtConfigurations(string Audience, string Issuer, string SecretKey);
}
