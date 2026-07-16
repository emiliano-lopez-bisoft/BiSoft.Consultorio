using BiSoft.Consultorio.Api.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class ActualizarSala
    {
        private const string ENDPOINT_NAME = "Actualizar Sala";
        public static RouteGroupBuilder MapActualizarSalaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{salaId}",
                    async (
                        [FromRoute] Guid salaId,
                        [FromBody] ActualizarSalaRequest request,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {

                        var sala = await salaService.ActualizarSala(salaId, request.Nombre);
                        return Results.Ok(sala);
                    }
                )
                .Produces<ActualizarSalaResponse>(StatusCodes.Status200OK)
                .WithDescription("Actualiza la información de una sala con su ID.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
