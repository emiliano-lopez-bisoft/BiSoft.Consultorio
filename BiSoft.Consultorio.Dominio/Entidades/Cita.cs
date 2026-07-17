using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Cita
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Motivo { get; set; }
        public Guid SalaId { get; set; }
        public Sala Sala { get; set; }
        public bool Activo { get; private set; } = true;
        protected Cita() { }

        public Cita(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, string motivo)
        {
            Id = Guid.NewGuid();
            Fecha = fecha;
            PacienteId = pacienteId;
            DoctorId = doctorId;
            Motivo = motivo;
            SalaId = salaId;
        }

        public void Desactivar() 
        { 
            Activo = false; 
        }

        public void Reagendar(DateTime nuevaFecha)
        {
            if(nuevaFecha < DateTime.Now)
            {
                throw new ArgumentException("No se puede reagendar una cita hacia el pasado.");
            }
            Fecha = nuevaFecha;
        }
    }
}
