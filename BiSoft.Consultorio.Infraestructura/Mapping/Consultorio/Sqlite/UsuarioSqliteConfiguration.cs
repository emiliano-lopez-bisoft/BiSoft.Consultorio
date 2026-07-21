using BiSoft.Consultorio.Dominio.Entidades.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Infraestructura.Mapping.Consultorio.Sqlite
{
    internal class UsuarioSqliteConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(u => u.NombreCompleto)
                .HasColumnName("nombreCompleto")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.NombreUsuario)
                .HasColumnName("nombreUsuario")
                .HasColumnType("TEXT")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("passwordHash")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(u => u.FechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(u => u.FechaActualizacion)
                .HasColumnName("fechaActualizacion")
                .HasColumnType("TEXT");

            builder.Property(u => u.Activo)
                .HasColumnName("activo")
                .HasColumnType("INTEGER")
                .IsRequired();

            // Índice único para el nombre de usuario
            builder.HasIndex(u => u.NombreUsuario).IsUnique();
        }
    }
}
