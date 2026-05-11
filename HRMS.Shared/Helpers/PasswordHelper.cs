using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Helpers
{
    public static class PasswordHelper
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); // generate 16 bytes salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));
            string saltString = Convert.ToBase64String(salt);
            return (hash, saltString);
        }

        public static bool VerifyPassword(string password, string storedHash, string salt)
        {
            byte[] storedSalt = Convert.FromBase64String(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash) == storedHash;
        }
    }
}
