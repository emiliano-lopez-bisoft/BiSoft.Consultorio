using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ConsultarTodosLosPacientes
    {
        public static RouteGroupBuilder MapConsultarTodosLosPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async ([FromServices] PacienteService pacienteService, CancellationToken ct) =>
            {
                var pacientes = await pacienteService.ConsultarTodosLosPacientes();
                return Results.Ok(pacientes);
            })
            .Produces<List<ConsultarPacienteResponse>>(StatusCodes.Status200OK)
            .WithDescription("Obtiene la lista de todos los pacientes activos en el sistema.")
            .WithSummary("Consultar Todos los Pacientes")
            .WithName("ConsultarTodosLosPacientes");

            return group;
        }
    }
}
