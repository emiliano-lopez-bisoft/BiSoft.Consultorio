using BiSoft.Consultorio.Dominio.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Paciente : Persona
    {
        public Paciente() : base() { }
        public Paciente(string nombre)
            : base(nombre)
        {
        }
    }
}
