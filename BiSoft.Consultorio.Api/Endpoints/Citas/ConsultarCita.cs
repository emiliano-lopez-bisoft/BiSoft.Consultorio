using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class ConsultarCita
    {
        private const string ENDPOINT_NAME = "Consultar Cita";
        public static RouteGroupBuilder MapConsultarCitaEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{citaId}",
                    async (
                        [FromRoute] Guid citaId,
                        CitaService citaService,
                        CancellationToken ct
                    ) =>
                    {
                        var cita = await citaService.ConsultarCita(citaId);
                        return Results.Ok(cita);
                    }
                )
                .Produces<ConsultarCitaResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta la información de una cita con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
