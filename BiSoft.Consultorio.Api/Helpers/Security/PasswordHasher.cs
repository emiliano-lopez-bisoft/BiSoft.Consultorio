using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using BiSoft.Consultorio.Dominio.Seguridad;

namespace BiSoft.Consultorio.Api.Helpers.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string passwordAttempt, string storedPasswordHash)
        {
            var elements = storedPasswordHash.Split(Delimiter);
            if (elements.Length != 2) return false;

            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashAttempt = Rfc2898DeriveBytes.Pbkdf2(passwordAttempt, salt, Iterations, _hashAlgorithmName, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashAttempt);
        }
    }
}
