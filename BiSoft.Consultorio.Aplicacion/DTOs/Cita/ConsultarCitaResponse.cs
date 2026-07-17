using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Cita
{
    public class ConsultarCitaResponse
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public Guid PacienteId { get; set; }
        public string PacienteNombre { get; set; } = string.Empty;
        public Guid DoctorId { get; set; }
        public string DoctorNombre { get; set; } = string.Empty;
        public Guid SalaId { get; set; }
        public string SalaNombre { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
    }
}
