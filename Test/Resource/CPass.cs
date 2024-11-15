using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
    public class CPass
    {
        public static string GenerateSalt(int size = 64)
        {
            var saltBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create()) 
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public static string HashPasswordWithSalt(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                return Convert.ToBase64String(hashedBytes); 
            }
        }
    }
}
