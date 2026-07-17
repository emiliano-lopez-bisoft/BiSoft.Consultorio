using BiSoft.Consultorio.Dominio.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; private set; } = true;

        protected Sala() { }
        public Sala(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre.ValidateEmpty("nombre");
        }

        public void Actualizar(string nombre)
        {
            Nombre = nombre.ValidateEmpty("nombre");
        }
        public void Desactivar() { Activo = false; }
    }
}
