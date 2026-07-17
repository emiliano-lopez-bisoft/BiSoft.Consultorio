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
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public ConsultorioContext(DbContextOptions<ConsultorioContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new SalaSqliteConfiguration());
            modelBuilder.ApplyConfiguration(new CitaSqliteConfiguration());

            modelBuilder.Entity<Doctor>().HasQueryFilter(d => d.Activo);
            modelBuilder.Entity<Paciente>().HasQueryFilter(p => p.Activo);
            modelBuilder.Entity<Sala>().HasQueryFilter(s => s.Activo);
            modelBuilder.Entity<Cita>().HasQueryFilter(c => c.Activo);

            base.OnModelCreating(modelBuilder);
        }
    }
}
