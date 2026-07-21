using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDomainService _usuarioDomainService;

        public UsuarioService(UsuarioDomainService usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public async Task<ConsultarUsuarioResponse> CrearUsuarioAsync(CrearUsuarioRequest request)
        {
            var usuario = await _usuarioDomainService.CrearUsuarioAsync(
                request.NombreCompleto,
                request.NombreUsuario,
                request.Password
            );

            return usuario.Adapt<ConsultarUsuarioResponse>();
        }

        public async Task<ConsultarUsuarioResponse> ActualizarUsuarioAsync(Guid id, ActualizarUsuarioRequest request)
        {
            var usuario = await _usuarioDomainService.ActualizarDatosAsync(
                id,
                request.NombreCompleto,
                request.NombreUsuario
            );

            return usuario.Adapt<ConsultarUsuarioResponse>();
        }

        public async Task ActualizarPasswordAsync(Guid id, ActualizarPasswordRequest request)
        {
            await _usuarioDomainService.ActualizarPasswordAsync(
                id,
                request.PasswordActual,
                request.NuevoPassword
            );
        }

        public async Task EliminarUsuarioAsync(Guid id)
        {
            await _usuarioDomainService.DesactivarUsuarioAsync(id);
        }

        public async Task<ConsultarUsuarioResponse> ValidarLoginAsync(string nombreUsuario, string password)
        {
            var usuario = await _usuarioDomainService.ValidarCredencialesAsync(nombreUsuario, password);
            return usuario.Adapt<ConsultarUsuarioResponse>();
        }
    }
}
