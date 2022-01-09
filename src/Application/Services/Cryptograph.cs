using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class Cryptograph
    {
        public static string GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder hash = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    hash.Append(data[i].ToString("x2"));

                return hash.ToString();
            }
        }

        public static bool HashCheck(string input, string hash)
        {
            string hashInput = GetHash(input);
            StringComparer checker = StringComparer.OrdinalIgnoreCase;
            return checker.Compare(hashInput, hash) == 0;
        }
    }
}