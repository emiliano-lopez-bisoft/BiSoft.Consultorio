using BiSoft.Consultorio.Api.DTOs.Configurations;
using BiSoft.Consultorio.Api.Helpers.HealthChecks;
using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;
using System.Threading.RateLimiting;

namespace BiSoft.Consultorio.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigurarServicios(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DataBaseConnections:Consultorio:ConnectionString"];

            services.AddScoped<DoctorService>();
            services.AddScoped<PacienteService>();
            services.AddScoped<SalaService>();
            services.AddScoped<CitaService>();
            services.AddScoped<DoctorDomainService>();
            services.AddScoped<PacienteDomainService>();
            services.AddScoped<SalaDomainService>();
            services.AddScoped<CitaDomainService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();
            services.AddDbContext<ConsultorioContext>(
                options => options.UseSqlite(connectionString)
            );

            return services;
        }

        public static IServiceCollection ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection ConfigurarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Program.CORS_POLICY_NAME, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            return services;
        }
        public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                    .AddCheck("Liveness", () => HealthCheckResult.Healthy($"API iniciada correctamente"))
                    .AddCheck("Database", new DataBaseHealthCheck(connectionString), tags: ["ready"]);
            return services;
        }
        public static IServiceCollection ConfigureRateLimiter(this IServiceCollection services, int allowedRequestsPerMinute)
        {
            services.AddRateLimiter(config =>
            {
                config.OnRejected = (context, ct) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                    }
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.WriteAsync("Demasiados requests. Intente más tarde.", cancellationToken: ct);
                    return new ValueTask();
                };
                config.AddFixedWindowLimiter(Program.RATE_LIMITER_POLICY_NAME, options =>
                {
                    options.PermitLimit = allowedRequestsPerMinute;
                    options.Window = TimeSpan.FromMinutes(1);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;
                });
            });
            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.SQLite(
                    sqliteDbPath: "Logs/Logs.db",
                    tableName: "Logs",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                 )
                 .WriteTo.Console()
                 .CreateLogger();
            services.AddSerilog();
            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, JwtConfigurations jwtConfiguration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "your-issuer",
                            ValidAudience = "your-audience",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key")),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            return services;
        }
    }
}
