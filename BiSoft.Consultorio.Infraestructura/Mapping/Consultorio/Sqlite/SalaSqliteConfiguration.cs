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
        }
    }
}
