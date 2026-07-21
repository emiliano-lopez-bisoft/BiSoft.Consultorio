namespace BiSoft.Consultorio.Api.DTOs.Configurations
{
    public record GeneralConfigurations
    (
      string ConsultorioConnectionString,
      string SeguridadConnectionString,
      int RateLimit,
      JwtConfiguration JWT
    );
}
