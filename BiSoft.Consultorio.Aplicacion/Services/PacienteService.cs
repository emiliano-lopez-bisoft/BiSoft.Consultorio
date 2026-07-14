using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Services;
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

        public async Task<Paciente> RegistrarPaciente(string nombre)
        {
            var paciente = await _pacienteDomainService.RegistrarPaciente(nombre);
            _logger.LogInformation("Paciente registrado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }
    }
}
