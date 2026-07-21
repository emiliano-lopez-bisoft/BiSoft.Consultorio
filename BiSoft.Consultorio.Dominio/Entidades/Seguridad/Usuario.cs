using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades.Seguridad
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string NombreCompleto { get; private set; } = string.Empty;
        public string NombreUsuario { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaActualizacion { get; private set; }
        public bool Activo { get; private set; }

        protected Usuario() { }

        public Usuario(string nombreCompleto, string nombreUsuario, string passwordHash)
        {
            Id = Guid.NewGuid();
            NombreCompleto = nombreCompleto;
            NombreUsuario = nombreUsuario;
            PasswordHash = passwordHash;
            FechaCreacion = DateTime.Now;
            Activo = true;
        }

        public void ActualizarDatos(string nombreCompleto, string nombreUsuario)
        {
            NombreCompleto = nombreCompleto;
            NombreUsuario = nombreUsuario;
            FechaActualizacion = DateTime.Now;
        }

        public void ActualizarPassword(string nuevoPasswordHash)
        {
            PasswordHash = nuevoPasswordHash;
            FechaActualizacion = DateTime.Now;
        }

        public void Desactivar()
        {
            Activo = false;
            FechaActualizacion = DateTime.Now;
        }
    }
}
