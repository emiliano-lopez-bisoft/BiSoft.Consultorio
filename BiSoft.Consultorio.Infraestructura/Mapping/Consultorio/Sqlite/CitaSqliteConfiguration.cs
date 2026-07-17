using BiSoft.Consultorio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class CitaSqliteConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Fecha)
                .HasColumnName("Fecha")
                .HasColumnType("TEXT")
                .IsRequired();

            // Permitir que sea nulo por si el motivo se da directo en consulta
            builder.Property(c => c.Motivo)
                .HasColumnName("Motivo")
                .HasColumnType("TEXT")
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(c => c.PacienteId)
                .HasColumnName("Paciente_Id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DoctorId)
                .HasColumnName("Doctor_Id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.SalaId)
                .HasColumnName("Sala_Id")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();

            // Mapeo de la eliminación lógica
            builder.Property(c => c.Activo)
                .HasColumnName("Activo")
                .HasColumnType("INTEGER")
                .IsRequired();

            // --- RELACIONES (Llaves Foráneas) ---

            builder.HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aquí le indicamos explícitamente a EF Core que use la propiedad de navegación
            builder.HasOne(c => c.Sala)
                .WithMany()
                .HasForeignKey(c => c.SalaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
