using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class EliminarUsuario
    {
        public static RouteGroupBuilder MapEliminarUsuario(this RouteGroupBuilder group)
        {
            group.MapDelete("{id:guid}", async (
                Guid id,
                [FromServices] UsuarioService usuarioService,
                CancellationToken ct) =>
            {
                try
                {
                    await usuarioService.EliminarUsuarioAsync(id);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Realiza una baja lógica del usuario en el sistema")
            .WithSummary("Eliminar Usuario")
            .WithName("EliminarUsuario");

            return group;
        }
    }
}
