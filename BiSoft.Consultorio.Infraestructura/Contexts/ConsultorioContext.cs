using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Contexts
{
    public class ConsultorioContext : DbContext
    {
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteSqliteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
