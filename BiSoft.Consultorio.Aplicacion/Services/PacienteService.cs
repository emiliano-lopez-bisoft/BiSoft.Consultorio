using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class PacienteService
    {
        private readonly ILogger<PacienteService> _logger;
        private readonly PacienteDomainService _pacienteDomainService;

        public PacienteService(ILogger<PacienteService> logger, PacienteDomainService pacienteDomainService)
        {
            _logger = logger;
            _pacienteDomainService = pacienteDomainService;
        }

        public async Task<List<ConsultarPacienteResponse>> ConsultarTodosLosPacientes()
        {
            var pacientes = await _pacienteDomainService.ConsultarTodosLosPacientes();
            return pacientes.Adapt<List<ConsultarPacienteResponse>>();
        }
        public async Task<Paciente> RegistrarPaciente(string nombre)
        {
            var paciente = await _pacienteDomainService.RegistrarPaciente(nombre);
            _logger.LogInformation("Paciente registrado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }

        public async Task<Paciente> ActualizarPaciente(Guid pacienteId, string nombre)
        {
            var paciente = await _pacienteDomainService.ActualizarPaciente(pacienteId, nombre);
            _logger.LogInformation("Paciente actualizado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }

        public async Task<ConsultarPacienteResponse> ConsultorPaciente(Guid pacienteId)
        {
            var paciente = await _pacienteDomainService.ObtenerPaciente(pacienteId);
            _logger.LogInformation("Paciente obtenido con id {}", pacienteId);
            return paciente.Adapt<ConsultarPacienteResponse>();
        }

        public async Task EliminarPaciente(Guid pacienteId)
        {
            await _pacienteDomainService.EliminarPaciente(pacienteId);
            _logger.LogInformation("Paciente eliminado con id {}", pacienteId);
        }
    }
}
