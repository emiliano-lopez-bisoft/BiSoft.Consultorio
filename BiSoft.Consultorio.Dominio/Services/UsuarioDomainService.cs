using BiSoft.Consultorio.Dominio.Entidades.Seguridad;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Services
{
    public class UsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Usuario> CrearUsuarioAsync(string nombreCompleto, string nombreUsuario, string passwordText)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);
            if (usuarioExistente != null)
                throw new Exception("El nombre de usuario ya está en uso.");

            var passwordHash = _passwordHasher.HashPassword(passwordText);

            var nuevoUsuario = new Usuario(nombreCompleto, nombreUsuario, passwordHash);
            await _usuarioRepository.AgregarAsync(nuevoUsuario);

            return nuevoUsuario;
        }

        public async Task<Usuario> ActualizarDatosAsync(Guid id, string nombreCompleto, string nuevoNombreUsuario)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id)
                ?? throw new Exception("Usuario no encontrado.");

            if (usuario.NombreUsuario != nuevoNombreUsuario)
            {
                var usuarioExistente = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nuevoNombreUsuario);
                if (usuarioExistente != null)
                    throw new Exception("El nuevo nombre de usuario ya está en uso.");
            }

            usuario.ActualizarDatos(nombreCompleto, nuevoNombreUsuario);
            await _usuarioRepository.ActualizarAsync(usuario);

            return usuario;
        }

        public async Task ActualizarPasswordAsync(Guid id, string passwordActual, string nuevoPassword)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id)
                ?? throw new Exception("Usuario no encontrado.");
            if (!_passwordHasher.VerifyPassword(passwordActual, usuario.PasswordHash))
                throw new Exception("La contraseña actual es incorrecta.");

            var nuevoPasswordHash = _passwordHasher.HashPassword(nuevoPassword);

            usuario.ActualizarPassword(nuevoPasswordHash);
            await _usuarioRepository.ActualizarAsync(usuario);
        }

        public async Task DesactivarUsuarioAsync(Guid id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id)
                ?? throw new Exception("Usuario no encontrado.");

            usuario.Desactivar();
            await _usuarioRepository.ActualizarAsync(usuario);
        }

        public async Task<Usuario> ValidarCredencialesAsync(string nombreUsuario, string passwordIntento)
        {
            var usuario = await _usuarioRepository.ObtenerPorNombreUsuarioAsync(nombreUsuario);

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (!_passwordHasher.VerifyPassword(passwordIntento, usuario.PasswordHash))
                throw new Exception("Usuario o contraseña incorrectos.");

            return usuario;
        }
    }
}
