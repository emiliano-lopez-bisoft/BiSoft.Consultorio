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
                var rateLimiting = builder.Configuration.GetValue<int>("RateLimiting");
                var connectionString = builder.Configuration["DatabaseConnections:Consultorio:ConnectionString"];

                // Inyeccion de servicios
                builder.Services.InyectarServicios()
                                .InyectarContextos(connectionString)
                                .ConfigurarSwagger()
                                .ConfigurarCors()
                                .ConfigurarHealthChecks(connectionString)
                                .ConfigureRateLimiter(rateLimiting)
                                .ContigureLogger();

                // Add services to the container.
                builder.Services.AddAuthorization();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseMiddleware<ErrorHandlerMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.AddHealthChecks(RATE_LIMITER_POLICY_NAME);

                app.MapEndpoints();

                // OpenApi
                app.UseSwagger();
                app.UseSwaggerUI();

                // CORS
                app.UseCors(CORS_POLICY_NAME);

                app.UseRateLimiter();

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
