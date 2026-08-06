using System.Security.Cryptography;
using System.Text;

namespace CRUDCORE.Datos
{
    public static class Seguridad
    {
        // Convierte una clave en texto plano a un hash SHA256 (en hexadecimal).
        // Así nunca se guarda ni se compara la contraseña real en la base de datos.
        public static string GenerarHash(string clave)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                var sb = new StringBuilder();
                foreach (var b in bytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}
