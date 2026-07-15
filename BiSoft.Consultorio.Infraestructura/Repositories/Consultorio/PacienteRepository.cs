using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ConsultorioContext _context;
        public PacienteRepository(ConsultorioContext context)
        {
            _context = context;
        }
        public async Task<Paciente?> ObtenerPaciente(Guid pacienteId)
        {
            return await _context.Pacientes.OrderBy(d => d.Id).FirstOrDefaultAsync(d => d.Id == pacienteId);
        }

        public IQueryable<Paciente> ConsultarPacientes ()
        {
            return _context.Pacientes;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task RegistrarPaciente(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
        }

        public async Task EliminarPaciente(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
        }
    }
}
