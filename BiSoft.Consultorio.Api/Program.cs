
using BiSoft.Consultorio.Api.DTOs.Doctor;
using BiSoft.Consultorio.Api.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BiSoft.Consultorio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var connectionString = builder.Configuration["DataBaseConnections:Consultorio:ConnectionString"];

                builder.Services.AddScoped<DoctorService>();
                builder.Services.AddScoped<PacienteService>();
                builder.Services.AddScoped<DoctorDomainService>();
                builder.Services.AddScoped<PacienteDomainService>();
                builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
                builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
                builder.Services.AddDbContext<ConsultorioContext>(
                    options => options.UseSqlite(connectionString)
                );

                Log.Logger = new LoggerConfiguration()
                             .WriteTo.SQLite(
                                sqliteDbPath: "Logs/Logs.db",
                                tableName: "Logs",
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                             )
                             .CreateLogger();
                //To do Agregar serilog


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

                app.UseHttpsRedirection();

                app.UseAuthorization();


                //CRUD DOCTOR
                app.MapGet("api/doctores/{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        try
                        {
                            var doctor = await doctorService.ConsultorDoctor(doctorId);
                            return Results.Ok(doctor);
                        }
                        catch (KeyNotFoundException)
                        {
                            return Results.NotFound("No se encontró el registro");
                        }
                    }
                )
                .WithSummary("Consultar Doctor")
                .WithName("Consultar Doctor");

                app.MapPost("api/doctores",
                    async (
                        [FromBody] RegistrarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        var doctor = await doctorService.RegistrarDoctor(request.Nombre, request.Especialidad);
                        return Results.Created($"api/doctores/{doctor.Id}", doctor);
                    }
                )
                .WithSummary("Registrar Doctor")
                .WithName("Registrar Doctor");

                app.MapPut("api/doctores/{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        [FromBody] ActualizarDoctorRequest request,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                       
                        var doctor = await doctorService.ActualizarDoctor(doctorId, request.Nombre, request.Especialidad);
                        return Results.Ok(doctor);
                    }
                )
                .WithSummary("Actualizar Doctor")
                .WithName("Actualizar Doctor");

                app.MapDelete("api/doctores/{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                    ) =>
                    {
                        await doctorService.EliminarDoctor(doctorId);
                        return Results.NoContent();
                    }
                )
                .WithSummary("Eliminar Doctor")
                .WithName("Eliminar Doctor");


                //CRUD PACIENTE
                app.MapGet("api/pacientes/{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        try
                        {
                            var paciente = await pacienteService.ConsultorPaciente(pacienteId);
                            return Results.Ok(paciente);
                        }
                        catch (KeyNotFoundException)
                        {
                            return Results.NotFound("No se encontró el registro");
                        }
                    }
                )
                .WithSummary("Consultar Paciente")
                .WithName("Consultar Paciente");

                app.MapPost("api/pacientes",
                    async (
                        [FromBody] RegistrarPacienteRequest request,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        var paciente = await pacienteService.RegistrarPaciente(request.Nombre);
                        return Results.Created($"api/pacientes/{paciente.Id}", paciente);
                    }
                )
                .WithSummary("Registrar Paciente")
                .WithName("Registrar Paciente");

                app.MapPut("api/pacientes/{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        [FromBody] ActualizarPacienteRequest request,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {

                        var paciente = await pacienteService.ActualizarPaciente(pacienteId, request.Nombre);
                        return Results.Ok(paciente);
                    }
                )
                .WithSummary("Actualizar Paciente")
                .WithName("Actualizar Paciente");

                app.MapDelete("api/pacientes/{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                    ) =>
                    {
                        await pacienteService.EliminarPaciente(pacienteId);
                        return Results.NoContent();
                    }
                )
                .WithSummary("Eliminar Paciente")
                .WithName("Eliminar Paciente");

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aplication start-up failed {ex.Message}");
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
