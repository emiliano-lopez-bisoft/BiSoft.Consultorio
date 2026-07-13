
using BiSoft.Consultorio.Dominio.Validators.Entidades;
using System;
using System.Globalization;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Doctor
    {
        public Guid Id { get; }
        public string Nombre { get; private set; }
        public string Especialidad { get; private set; }
        private Doctor() { }
        public Doctor(string nombre, string especialidad)
        {
            Id = Guid.NewGuid();
            Nombre = nombre.ValidateNombre();
            Especialidad = especialidad;
        }
        public void Actualizar(string nombre, string especialidad) 
        {
            Nombre = nombre.ValidateNombre();
            Especialidad = especialidad.ValidateEspecialidad();
        }
    }
}
