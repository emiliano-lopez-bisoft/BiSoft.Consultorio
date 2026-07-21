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
    public class SalaDomainService
    {
        private readonly ILogger<SalaDomainService> _logger;
        private readonly ISalaRepository _salaRepository;

        public SalaDomainService(ILogger<SalaDomainService> logger, ISalaRepository salaRepository)
        {
            _logger = logger;
            _salaRepository = salaRepository;
        }

        public async Task<List<Sala>> ConsultarTodasLasSalas()
        {
            return await _salaRepository.ObtenerTodasLasSalas();
        }
        public async Task<Sala> RegistrarSala(string nombre)
        {
            var sala = new Sala(nombre);
            await _salaRepository.RegistrarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala registrada: {SalaNombre}", sala.Nombre);
            return sala;
        }
        public async Task<Sala> ActualizarSala(Guid salaId, string nombre)
        {
            var sala = await ObtenerSala(salaId);
            sala.Actualizar(nombre);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala actualizada: {SalaNombre}", sala.Nombre);
            return sala;
        }
        public async Task<Sala> ObtenerSala(Guid salaId)
        {
            var sala = await _salaRepository.ObtenerSala(salaId) ?? throw new Exception($"No se encontró el sala con id {salaId}.");
            _logger.LogInformation("Sala obtenida: {SalaNombre}", sala.Nombre);
            return sala;
        }

        public IQueryable<Sala> ConsultarSalas()
        {
            var salas = _salaRepository.ConsultarSalas();
            _logger.LogInformation($"Salas consultadas");
            return salas;
        }
        public async Task EliminarSala(Guid salaId)
        {
            var sala = await ObtenerSala(salaId);
            await _salaRepository.EliminarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala eliminada: {SalaNombre}", sala.Nombre);
        }
    }
}
