using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class ActualizarUsuario
    {
        public static RouteGroupBuilder MapActualizarUsuario(this RouteGroupBuilder group)
        {
            group.MapPut("{id:guid}", async (
                Guid id,
                [FromBody] ActualizarUsuarioRequest request,
                [FromServices] UsuarioService usuarioService,
                CancellationToken ct) =>
            {
                try
                {
                    var response = await usuarioService.ActualizarUsuarioAsync(id, request);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .Produces<ConsultarUsuarioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Actualiza el nombre completo y nombre de usuario")
            .WithSummary("Actualizar Perfil de Usuario")
            .WithName("ActualizarUsuario");

            return group;
        }
    }
}
