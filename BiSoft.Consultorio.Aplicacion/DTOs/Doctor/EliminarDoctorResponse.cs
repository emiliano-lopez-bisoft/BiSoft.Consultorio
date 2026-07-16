using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Doctor
{
    public class EliminarDoctorResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
    }
}
