using BiSoft.Consultorio.Api.DTOs.Configurations;
using BiSoft.Consultorio.Api.Extensions;
using BiSoft.Consultorio.Api.Extensions.Endpoints;
using BiSoft.Consultorio.Api.Middlewares;
using Serilog;

namespace BiSoft.Consultorio.Api
{
    public static class Program
    {
        public const string RATE_LIMITER_POLICY_NAME = "Fixed";
        public const string CORS_POLICY_NAME = "allowAll";
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var configuration = builder.Configuration.GetGeneralConfigurations();

                // Inyeccion de Servicios
                builder.Services.AddSingleton(configuration.JWT);
                builder.Services.InyectarServicios()
                                .InyectarContextos(configuration.ConsultorioConnectionString, configuration.SeguridadConnectionString)
                                .ConfigurarSwagger()
                                .ConfigurarCors()
                                .ConfigurarHealthChecks(configuration.ConsultorioConnectionString, configuration.SeguridadConnectionString)
                                .ConfigureRateLimiter(configuration.RateLimit)
                                .ContigureLogger()
                                .ConfigureAuthentication(configuration.JWT);

                // Add services to the container.
                builder.Services.AddAuthorization();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();

                var app = builder.Build();
                // CORS
                app.UseCors(CORS_POLICY_NAME);
                app.UseRateLimiter();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMiddleware<ErrorHandlerMiddleware>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseHttpsRedirection();

                app.AddHealthChecks(RATE_LIMITER_POLICY_NAME);

                app.MapEndpoints();

                // OpenApi
                app.UseSwagger();
                app.UseSwaggerUI();              


                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application start-up failed: {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
