using BiSoft.Consultorio.Dominio.Entidades.Seguridad;
using BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Contexts
{
    public class SeguridadContext : DbContext
    {
        public SeguridadContext(DbContextOptions<SeguridadContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioSqliteConfiguration());
        }
    }
}
