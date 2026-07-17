using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Cita
{
    public class EliminarCitaResponse
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid SalaId { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
