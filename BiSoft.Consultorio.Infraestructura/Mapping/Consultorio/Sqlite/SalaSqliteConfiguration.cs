using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class SalaSqliteConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Salas")
                .HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(s => s.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Activo)
                .HasColumnName("activo")
                .HasColumnType("INTEGER")
                .IsRequired();
        }
    }
}
