using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Demo.Application.Core
{
    public static class Encrypt
    {
        public static string ConvertToSHA512(string value)
        {
            using var sha = SHA512.Create();

            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
