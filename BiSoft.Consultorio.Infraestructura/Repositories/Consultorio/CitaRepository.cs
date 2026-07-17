using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ConsultorioContext _context;

        public CitaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public async Task<Cita?> ObtenerCita(Guid citaId)
        {
            return await _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(c => c.Id == citaId);
        }

        public IQueryable<Cita> ConsultarCitas()
        {
            return _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .Include(c => c.Sala);
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task RegistrarCita(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
        }
        public async Task EliminarCita(Cita cita)
        {
            cita.Desactivar();
            _context.Citas.Update(cita);
        }
    }
}
