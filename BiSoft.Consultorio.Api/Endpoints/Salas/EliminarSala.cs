using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class EliminarSala
    {
        private const string ENDPOINT_NAME = "Eliminar Sala";

        public static RouteGroupBuilder MapEliminarSalaEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{salaId}",
                    async (
                        [FromRoute] Guid salaId,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {
                        await salaService.EliminarSala(salaId);
                        return Results.NoContent();
                    }
                )
                .Produces<EliminarSalaResponse>(StatusCodes.Status204NoContent)
                .WithDescription("Elimina un sala con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
