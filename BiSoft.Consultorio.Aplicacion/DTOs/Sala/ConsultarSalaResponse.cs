using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Sala
{
    public class ConsultarSalaResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
