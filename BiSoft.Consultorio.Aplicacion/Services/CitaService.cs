using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class CitaService
    {
        private readonly ILogger<CitaService> _logger;
        private readonly CitaDomainService _citaDomainService;

        public CitaService(ILogger<CitaService> logger, CitaDomainService citaDomainService)
        {
            _logger = logger;
            _citaDomainService = citaDomainService;
        }

        public async Task<List<ConsultarCitaResponse>> ConsultarTodasLasCitas()
        {
            var citas = await _citaDomainService.ConsultarTodasLasCitas();
            return citas.Adapt<List<ConsultarCitaResponse>>();
        }
        public async Task<Cita> RegistrarCita(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, string motivo)
        {
            var cita = await _citaDomainService.RegistrarCita(fecha, pacienteId, doctorId, salaId, motivo);

            _logger.LogInformation("Cita registrada: {CitaId} - Fecha: {CitaFecha} - Doctor: {DoctorId}", cita.Id, cita.Fecha, cita.DoctorId);

            return cita;
        }

        public async Task<ConsultarCitaResponse> ConsultarCita(Guid citaId)
        {
            var cita = await _citaDomainService.ObtenerCita(citaId);
            _logger.LogInformation("Cita obtenida con id {CitaId}", citaId);
            return cita.Adapt<ConsultarCitaResponse>();
        }

        public async Task EliminarCita(Guid citaId)
        {
            await _citaDomainService.EliminarCita(citaId);
            _logger.LogInformation("Cita eliminada con id {CitaId}", citaId);
        }

        public async Task<ConsultarCitaResponse> ReagendarCita(Guid citaId, DateTime nuevaFecha)
        {
            var cita = await _citaDomainService.ReagendarCita(citaId, nuevaFecha);
            return cita.Adapt<ConsultarCitaResponse>();
        }
    }
}
