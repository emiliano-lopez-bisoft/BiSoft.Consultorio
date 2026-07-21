using BiSoft.Consultorio.Api.DTOs.Configurations;
using BiSoft.Consultorio.Api.DTOs.Security;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiSoft.Consultorio.Api.Endpoints.Security
{
    public static class Login
    {
        private const string ENDPOINT_NAME = "Login";
        
        public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
        {
            group.MapPost("login", [AllowAnonymous]
                    async (
                        JwtConfiguration jwtConfiguration,
                        [FromBody] LoginRequest request,
                        [FromServices] UsuarioService usuarioService,
                        CancellationToken ct
                    ) =>
                    {
                        try
                        {
                            var usuarioAutenticado = await usuarioService.ValidarLoginAsync(request.Usuario, request.Password);
                            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
                            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, usuarioAutenticado.Id.ToString()),
                                new Claim(ClaimTypes.Name, usuarioAutenticado.NombreUsuario)
                            };

                            var tokenOptions = new JwtSecurityToken(
                                issuer: jwtConfiguration.Issuer,
                                audience: jwtConfiguration.Audience,
                                expires: DateTime.Now.AddMinutes(15),
                                claims: claims,
                                signingCredentials: credentials
                                );

                            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                            return Results.Ok(new LoginResponse { Token = token });
                        }
                        catch (Exception)
                        {
                            return Results.Unauthorized();
                        }
                    }
                )
                .Produces<LoginResponse>(StatusCodes.Status200OK)
                .WithDescription("Permite iniciar sesión")
                .WithSummary(ENDPOINT_NAME)
                .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
