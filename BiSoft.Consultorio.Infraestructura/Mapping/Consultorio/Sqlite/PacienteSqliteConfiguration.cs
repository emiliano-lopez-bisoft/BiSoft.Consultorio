using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class PacienteSqliteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes")
                .HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Activo)
                .HasColumnName("activo")
                .HasColumnType("INTEGER")
                .IsRequired();
        }
    }
}
