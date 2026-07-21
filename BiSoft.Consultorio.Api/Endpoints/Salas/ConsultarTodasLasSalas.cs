using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class ConsultarTodasLasSalas
    {
        public static RouteGroupBuilder MapConsultarTodasLasSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async ([FromServices] SalaService salaService, CancellationToken ct) =>
            {
                var salas = await salaService.ConsultarTodasLasSalas();
                return Results.Ok(salas);
            })
            .Produces<List<ConsultarSalaResponse>>(StatusCodes.Status200OK)
            .WithDescription("Obtiene la lista de todas las salas activas en el sistema.")
            .WithSummary("Consultar Todas las Salas")
            .WithName("ConsultarTodasLasSalas");

            return group;
        }
    }
}
