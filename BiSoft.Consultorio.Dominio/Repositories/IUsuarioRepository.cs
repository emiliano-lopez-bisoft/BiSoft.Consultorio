using BiSoft.Consultorio.Dominio.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario?> ObtenerPorIdAsync(Guid id);
        Task AgregarAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
    }
}
