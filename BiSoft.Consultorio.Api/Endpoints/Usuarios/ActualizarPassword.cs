using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class ActualizarPassword
    {
        public static RouteGroupBuilder MapActualizarPassword(this RouteGroupBuilder group)
        {
            group.MapPut("{id:guid}/password", async (
                Guid id,
                [FromBody] ActualizarPasswordRequest request,
                [FromServices] UsuarioService usuarioService,
                CancellationToken ct) =>
            {
                try
                {
                    await usuarioService.ActualizarPasswordAsync(id, request);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Actualiza la contraseña validando la credencial actual")
            .WithSummary("Actualizar Contraseña")
            .WithName("ActualizarPassword");

            return group;
        }
    }
}
