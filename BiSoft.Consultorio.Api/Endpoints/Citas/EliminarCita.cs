using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class EliminarCita
    {
        private const string ENDPOINT_NAME = "Eliminar Cita";

        public static RouteGroupBuilder MapEliminarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        await citaService.EliminarCita(citaId);
                        return Results.NoContent();
                    }
                )
                .Produces<EliminarCitaResponse>(StatusCodes.Status204NoContent)
                .WithDescription("Elimina lógicamente una cita con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
