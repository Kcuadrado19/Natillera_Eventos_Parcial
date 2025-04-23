using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Natillera_Eventos_Parcial.Clases
{
    public class clsCypher
    {
        // Generar una sal aleatoria (array de bytes)
        public static byte[] GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        // Cifrar la contraseña con el salt usando SHA512
        public string HashPassword(string clave, byte[] salt)
        {
            using (var sha512 = SHA512.Create())
            {
                var claveBytes = Encoding.UTF8.GetBytes(clave);
                var claveConSalt = salt.Concat(claveBytes).ToArray();
                var hash = sha512.ComputeHash(claveConSalt);
                return Convert.ToBase64String(hash);
            }
        }
    }
}