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
    public class PacienteDomainService
    {
        private readonly ILogger<PacienteDomainService> _logger;
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteDomainService(ILogger<PacienteDomainService> logger, IPacienteRepository pacienteRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<Paciente> RegistrarPaciente(string nombre)
        {
            var paciente = new Paciente(nombre);
            await _pacienteRepository.RegistrarPaciente(paciente);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Paciente registrado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }
        public async Task<Paciente> ActualizarPaciente(Guid pacienteId, string nombre)
        {
            var paciente = await ObtenerPaciente(pacienteId);
            paciente.Actualizar(nombre);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Paciente actualizado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }
        public async Task<Paciente> ObtenerPaciente(Guid pacienteId)
        {
            var paciente = await _pacienteRepository.ConsultarPaciente(pacienteId) ?? throw new Exception($"No se encontró el paciente con id {pacienteId}.");
            _logger.LogInformation("Paciente obtenido: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }

        public IQueryable<Paciente> ConsultarPacientes()
        {
            var pacientes = _pacienteRepository.ConsultarPaciente();
            _logger.LogInformation($"Pacientes consultados");
            return pacientes;
        }
    }
}
