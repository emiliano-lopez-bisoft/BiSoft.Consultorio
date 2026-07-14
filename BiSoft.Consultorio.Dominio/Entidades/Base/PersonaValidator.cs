using BiSoft.Consultorio.Dominio.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades.Base
{
    public static class PersonaValidator
    {
        public static string ValidateNombre(this string nombre)
        {
            var nombreParametro = "nombre";
            nombre = nombre.Trim()
                            .ValidateEmpty(nombreParametro)
                            .ValidateLength(nombreParametro, 5, 50);

            if (!nombre.Contains(' '))
            {
                throw new ArgumentException("El nombre debe incluir nombre y apellido.");
            }
            return nombre;
        }
    }
}
