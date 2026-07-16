using BiSoft.Consultorio.Api.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class RegistrarPaciente
    {
        private const string ENDPOINT_NAME = "Registrar Paciente";

        public static RouteGroupBuilder MapRegistrarPacienteEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
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
                .Produces<RegistrarPacienteResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra un nuevo paciente en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
