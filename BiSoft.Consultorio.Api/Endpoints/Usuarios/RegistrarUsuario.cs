using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiSoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class RegistrarUsuario
    {
        public static RouteGroupBuilder MapRegistrarUsuario(this RouteGroupBuilder group)
        {
            group.MapPost("", [AllowAnonymous] async (
                [FromBody] CrearUsuarioRequest request,
                [FromServices] UsuarioService usuarioService,
                CancellationToken ct) =>
            {
                try
                {
                    var response = await usuarioService.CrearUsuarioAsync(request);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .Produces<ConsultarUsuarioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDescription("Registra un nuevo usuario en el sistema")
            .WithSummary("Registrar Usuario")
            .WithName("RegistrarUsuario");

            return group;
        }
    }
}
