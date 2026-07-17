using BiSoft.Consultorio.Api.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class ReagendarCita
    {
        private const string ENDPOINT_NAME = "Reagendar Cita";

        public static RouteGroupBuilder MapReagendarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{citaId}/reagendar",
                    async (
                        [FromRoute] Guid citaId,
                        [FromBody] ReagendarCitaRequest request,
                        [FromServices] CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var citaActualizada = await citaService.ReagendarCita(citaId, request.NuevaFecha);
                        return Results.Ok(citaActualizada);
                    }
                )
                .Produces<ConsultarCitaResponse>(StatusCodes.Status200OK)
                .WithDescription("Modifica únicamente la fecha y hora de una cita existente, validando empalmes.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);

            return group;
        }
    }
}
