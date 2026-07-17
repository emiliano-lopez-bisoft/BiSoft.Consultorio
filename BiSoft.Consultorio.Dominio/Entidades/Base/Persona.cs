using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades.Base
{
    public abstract class Persona
    {
        public Guid Id { get; }
        public string Nombre { get; private set; }
        public bool Activo { get; private set; } = true;
        protected Persona() { }
        protected Persona(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre.ValidateNombre();
        }

        public void Actualizar(string nombre)
        {
            Nombre = nombre.ValidateNombre();
        }
        public void Desactivar() { Activo = false; }
    }
}
