using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class SalaRepository : ISalaRepository
    {
        private readonly ConsultorioContext _context;
        public SalaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public async Task<List<Sala>> ObtenerTodasLasSalas()
        {
            return await _context.Salas
                .Where(d => d.Activo)
                .ToListAsync();
        }
        public async Task<Sala?> ObtenerSala(Guid salaId)
        {
            return await _context.Salas.OrderBy(d => d.Id).FirstOrDefaultAsync(d => d.Id == salaId);
        }

        public IQueryable<Sala> ConsultarSalas()
        {
            return _context.Salas;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task RegistrarSala(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
        }

        public async Task EliminarSala(Sala sala)
        {
            sala.Desactivar();
            _context.Salas.Update(sala);
        }
    }
}
