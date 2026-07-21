using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Seguridad
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordAttempt, string storedPasswordHash);
    }
}
