using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Usuario
{
    public class ActualizarPasswordRequest
    {
        public string PasswordActual { get; set; } = string.Empty;
        public string NuevoPassword { get; set; } = string.Empty;
    }
}
