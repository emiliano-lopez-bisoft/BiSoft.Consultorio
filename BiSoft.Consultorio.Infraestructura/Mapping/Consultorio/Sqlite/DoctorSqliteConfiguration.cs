using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class DoctorSqliteConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctores")
                .HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id")
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("NVARCHAR")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.Especialidad)
                .HasColumnName("especialidad")
                .HasColumnType("NVARCHAR")
                .IsRequired()
                .HasMaxLength(100);
        }

    }
}
