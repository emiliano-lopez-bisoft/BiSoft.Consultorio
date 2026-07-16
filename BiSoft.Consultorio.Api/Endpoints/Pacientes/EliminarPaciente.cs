using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class EliminarPaciente
    {
        private const string ENDPOINT_NAME = "Eliminar Paciente";

        public static RouteGroupBuilder MapEliminarPacienteEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{pacienteId}",
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
                .Produces<EliminarPacienteResponse>(StatusCodes.Status204NoContent)
                .WithDescription("Elimina un paciente con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
