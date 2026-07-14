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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorSqliteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
