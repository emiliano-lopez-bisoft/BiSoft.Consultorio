using BiSoft.Consultorio.Api.DTOs.Configurations;
using BiSoft.Consultorio.Api.Helpers.HealthChecks;
using BiSoft.Consultorio.Api.Helpers.Security;
using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Seguridad;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using BiSoft.Consultorio.Infraestructura.Repositories.Security;
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
        public static IServiceCollection InyectarServicios(this IServiceCollection services)
        {
            services.AddScoped<SalaService>();
            services.AddScoped<CitaService>();
            services.AddScoped<DoctorService>();
            services.AddScoped<PacienteService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<SalaDomainService>();
            services.AddScoped<CitaDomainService>();
            services.AddScoped<DoctorDomainService>();
            services.AddScoped<PacienteDomainService>();
            services.AddScoped<UsuarioDomainService>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }
        public static IServiceCollection InyectarContextos(this IServiceCollection services, 
            string consultorioConnectionString, string seguridadConnectionString)
        {
            services.AddDbContext<SeguridadContext>(options => options.UseSqlite(seguridadConnectionString));
            services.AddDbContext<ConsultorioContext>(options => options.UseSqlite(consultorioConnectionString));
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
        public static IServiceCollection ConfigurarHealthChecks(this IServiceCollection services, 
            string consultorioConnectionString, string seguridadConnectionString)
        {
            services.AddHealthChecks()
                    .AddCheck("Liveness", () => HealthCheckResult.Healthy($"API iniciada correctamente"))
                    .AddCheck("Database_Consultorio", new DataBaseHealthCheck(consultorioConnectionString), tags: ["ready"])
                    .AddCheck("Database_Seguridad", new DataBaseHealthCheck(seguridadConnectionString), tags: ["ready"]);
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
                    context.HttpContext.Response.WriteAsync("Demasiados request. Intente más tarde", cancellationToken: ct);
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
        public static IServiceCollection ContigureLogger(this IServiceCollection services)
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
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, JwtConfiguration jwtConfig)
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
                            ValidIssuer = jwtConfig.Issuer,
                            ValidAudience = jwtConfig.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            return services;
        }
    }
}
