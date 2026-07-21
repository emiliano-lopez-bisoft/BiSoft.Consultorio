using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Citas
{
    public static class ConsultarTodasLasCitas
    {
        public static RouteGroupBuilder MapConsultarTodasLasCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async ([FromServices] CitaService citaService, CancellationToken ct) =>
            {
                var citas = await citaService.ConsultarTodasLasCitas();
                return Results.Ok(citas);
            })
            .Produces<List<ConsultarCitaResponse>>(StatusCodes.Status200OK)
            .WithDescription("Obtiene la lista de todas las citas activas en el sistema.")
            .WithSummary("Consultar Todas las Citas")
            .WithName("ConsultarTodasLasCitas");

            return group;
        }
    }
}
