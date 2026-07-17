using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Services
{
    public class CitaDomainService
    {
        private readonly ILogger<CitaDomainService> _logger;
        private readonly ICitaRepository _citaRepository;

        public CitaDomainService(ILogger<CitaDomainService> logger, ICitaRepository citaRepository)
        {
            _logger = logger;
            _citaRepository = citaRepository;
        }

        public async Task<Cita> RegistrarCita(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, string motivo)
        {
            // 1. Extraemos las citas de ese mismo día para validar empalmes
            var fechaInicioDia = fecha.Date;
            var fechaFinDia = fechaInicioDia.AddDays(1);

            var citasDelDia = _citaRepository.ConsultarCitas()
                .Where(c => c.Fecha >= fechaInicioDia && c.Fecha < fechaFinDia && c.Activo)
                .ToList();

            // 2. Ejecutamos las validaciones de negocio antes de crear la entidad
            ValidarReglasDeAgendamiento(fecha, pacienteId, doctorId, salaId, citasDelDia);

            // 3. Instanciamos, guardamos y registramos el log tal como en DoctorDomainService
            var cita = new Cita(fecha, pacienteId, doctorId, salaId, motivo);
            await _citaRepository.RegistrarCita(cita);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita registrada: {CitaId} - Fecha: {CitaFecha} - Doctor: {DoctorId}", cita.Id, cita.Fecha, cita.DoctorId);

            return cita;
        }

        public async Task<Cita> ObtenerCita(Guid citaId)
        {
            var cita = await _citaRepository.ObtenerCita(citaId)
                ?? throw new KeyNotFoundException($"No se encontró la cita con id {citaId}.");

            _logger.LogInformation("Cita obtenida: {CitaId} - Fecha: {CitaFecha}", cita.Id, cita.Fecha);
            return cita;
        }

        public IQueryable<Cita> ConsultarCitas()
        {
            var citas = _citaRepository.ConsultarCitas();
            _logger.LogInformation("Citas consultadas");
            return citas;
        }

        public async Task EliminarCita(Guid citaId)
        {
            var cita = await ObtenerCita(citaId);
            await _citaRepository.EliminarCita(cita);
            await _citaRepository.GuardarCambios();
            _logger.LogInformation("Cita eliminada: {CitaId} - Fecha: {CitaFecha}", cita.Id, cita.Fecha);
        }

        public async Task<Cita> ReagendarCita(Guid citaId, DateTime nuevaFecha)
        {
            var cita = await ObtenerCita(citaId);
            if (cita.Fecha.Date <= DateTime.Now.Date)
            {
                throw new InvalidOperationException("Las citas no se pueden modificar el mismo día programado. Deberá cancelarse.");
            }

            // Extraemos las citas del NUEVO día para validar empalmes
            var fechaInicioDia = nuevaFecha.Date;
            var fechaFinDia = fechaInicioDia.AddDays(1);

            // Obtenemos las citas, EXCLUYENDO la cita actual (para que no choque consigo misma si se cambia a otra hora del mismo día)
            var citasDelDia = _citaRepository.ConsultarCitas()
                .Where(c => c.Fecha >= fechaInicioDia && c.Fecha < fechaFinDia && c.Activo && c.Id != citaId)
                .ToList();

            // Reutilizamos la validación de negocio de 30 minutos, horarios de 6 a 18 y empalmes
            ValidarReglasDeAgendamiento(nuevaFecha, cita.PacienteId, cita.DoctorId, cita.SalaId, citasDelDia);

            // Actualizamos la entidad y guardamos
            cita.Reagendar(nuevaFecha);
            await _citaRepository.GuardarCambios();

            _logger.LogInformation("Cita reagendada: {CitaId} - Nueva Fecha: {NuevaFecha}", cita.Id, cita.Fecha);
            return cita;
        }

        // --- MÉTODOS PRIVADOS DE VALIDACIÓN ---
        private void ValidarReglasDeAgendamiento(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, List<Cita> citasDelDia)
        {
            if (fecha.Minute != 0 && fecha.Minute != 30)
                throw new ArgumentException("Las citas deben agendarse en intervalos exactos de 30 minutos.");

            if (fecha.Hour < 6 || fecha.Hour >= 18)
                throw new ArgumentException("El horario de atención es de 6:00 a 18:00 hrs.");

            if (fecha.Hour == 17 && fecha.Minute > 30)
                throw new ArgumentException("La última cita del día debe comenzar a las 17:30 hrs.");

            foreach (var citaExistente in citasDelDia)
            {
                if (citaExistente.Fecha == fecha)
                {
                    if (citaExistente.DoctorId == doctorId)
                        throw new InvalidOperationException("El doctor ya tiene una cita asignada en este horario.");

                    if (citaExistente.PacienteId == pacienteId)
                        throw new InvalidOperationException("El paciente ya cuenta con una cita en este horario.");

                    if (citaExistente.SalaId == salaId)
                        throw new InvalidOperationException("La sala ya está reservada en este horario.");
                }
            }
        }
    }
}
