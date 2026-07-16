using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DataBaseConnections:Consultorio:ConnectionString"];

            services.AddScoped<DoctorService>();
            services.AddScoped<PacienteService>();
            services.AddScoped<SalaService>();
            services.AddScoped<DoctorDomainService>();
            services.AddScoped<PacienteDomainService>();
            services.AddScoped<SalaDomainService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddDbContext<ConsultorioContext>(
                options => options.UseSqlite(connectionString)
            );

            return services;
        }
    }
}
