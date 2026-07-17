namespace BiSoft.Consultorio.Api.DTOs.Cita
{
    public class RegistrarCitaRequest
    {
        public DateTime Fecha { get; set; }
        public Guid PacienteId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid SalaId { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }
}
