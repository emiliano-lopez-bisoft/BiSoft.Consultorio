using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Paciente
{
    public class RegistrarPacienteResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
