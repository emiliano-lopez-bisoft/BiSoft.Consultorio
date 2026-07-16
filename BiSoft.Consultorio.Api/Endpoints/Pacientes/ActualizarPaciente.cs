using BiSoft.Consultorio.Api.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ActualizarPaciente
    {
        private const string ENDPOINT_NAME = "Actualizar Paciente";
        public static RouteGroupBuilder MapActualizarPacienteEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{pacienteId}",
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
                .Produces<ActualizarPacienteResponse>(StatusCodes.Status200OK)
                .WithDescription("Actualiza la información de un paciente con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
