using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class SalaService
    {
        private readonly ILogger<SalaService> _logger;
        private readonly SalaDomainService _salaDomainService;

        public SalaService(ILogger<SalaService> logger, SalaDomainService salaDomainService)
        {
            _logger = logger;
            _salaDomainService = salaDomainService;
        }

        public async Task<Sala> RegistrarSala(string nombre)
        {
            var sala = await _salaDomainService.RegistrarSala(nombre);
            _logger.LogInformation("Sala registrado: {SalaNombre}", sala.Nombre);
            return sala;
        }

        public async Task<Sala> ActualizarSala(Guid salaId, string nombre)
        {
            var sala = await _salaDomainService.ActualizarSala(salaId, nombre);
            _logger.LogInformation("Sala actualizado: {SalaNombre}", sala.Nombre);
            return sala;
        }

        public async Task<ConsultarSalaResponse> ConsultorSala(Guid salaId)
        {
            var sala = await _salaDomainService.ObtenerSala(salaId);
            _logger.LogInformation("Sala obtenido con id {}", salaId);
            return sala.Adapt<ConsultarSalaResponse>();
        }

        public async Task EliminarSala(Guid salaId)
        {
            await _salaDomainService.EliminarSala(salaId);
            _logger.LogInformation("Sala eliminado con id {}", salaId);
        }
    }
}
