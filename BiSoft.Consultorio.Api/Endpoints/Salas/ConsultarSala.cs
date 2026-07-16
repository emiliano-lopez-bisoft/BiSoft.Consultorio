using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class ConsultarSala
    {
        private const string ENDPOINT_NAME = "Consultar Sala";

        public static RouteGroupBuilder MapConsultarSalaEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{salaId}",
                    async (
                        [FromRoute] Guid salaId,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {
                        var sala = await salaService.ConsultorSala(salaId);
                        return Results.Ok(sala);
                    }
                )
                .Produces<ConsultarSalaResponse>(StatusCodes.Status200OK)
                .WithDescription("Consulta la información de un sala con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
