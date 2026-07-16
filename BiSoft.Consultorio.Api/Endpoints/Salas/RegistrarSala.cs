using BiSoft.Consultorio.Api.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Salas
{
    public static class RegistrarSala
    {
        private const string ENDPOINT_NAME = "Registrar Sala";
        public static RouteGroupBuilder MapRegistrarSalaEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                    async (
                        [FromBody] RegistrarSalaRequest request,
                        SalaService salaService,
                        CancellationToken ct
                    ) =>
                    {
                        var sala = await salaService.RegistrarSala(request.Nombre);
                        return Results.Created($"api/salas/{sala.Id}", sala);
                    }
                )
                .Produces<RegistrarSalaResponse>(StatusCodes.Status201Created)
                .WithDescription("Registra una nueva sala en el sistema.")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
