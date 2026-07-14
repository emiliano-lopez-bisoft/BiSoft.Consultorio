using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Doctor
{
    public class RegistrarDoctorResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
    }
}
