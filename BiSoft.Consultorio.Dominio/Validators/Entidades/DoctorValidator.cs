using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators.Entidades
{
    public static class DoctorValidator
    {
        public static string ValidateNombre(this string nombre)
        {
            var nombreParametro = "nombre";
            nombre = nombre.Trim()
                            .ValidateEmpty(nombreParametro)
                            .ValidateLength(nombreParametro,5,50);

            if (!nombre.Contains(' '))
            {
                throw new ArgumentException("El nombre del doctor debe incluir nombre y apellido.");
            }
            return nombre;
        }

        public static string ValidateEspecialidad(this string especialidad)
        {
            var nombreParametro = "especialidad";
            especialidad = especialidad.Trim()
                                        .ValidateEmpty(nombreParametro)
                                        .ValidateLength(nombreParametro, 5, 100);
            return especialidad;
        }
    }
}
