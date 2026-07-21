namespace BiSoft.Consultorio.Api.DTOs.Configurations
{
    public record JwtConfiguration(string Audience, string Issuer, string SecretKey);
}
