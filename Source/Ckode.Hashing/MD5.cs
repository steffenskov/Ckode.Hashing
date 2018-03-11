using System.Security.Cryptography;
using System.Text;

namespace Ckode.Hashing
{
    /// <summary>
    /// Contains MD5 functions.
    /// </summary>
    public class MD5 : IHashingAlgorithm
    {
        public string CreateHash(string input)
        {
            return HashBytes(Encoding.Unicode.GetBytes(input));
        }

        public bool IsThisAlgorithm(string correctHash)
        {
            return correctHash?.Length == 32;
        }

        public bool ValidateHash(string input, string correctHash)
        {
            return CreateHash(input) == correctHash;
        }

        private string HashBytes(byte[] bytes)
        {
            using (var hasher = new MD5CryptoServiceProvider())
            {
                var result = hasher.ComputeHash(bytes);
                var sb = new StringBuilder(50);
                foreach (var bt in result)
                {
                    sb.Append(bt.ToString("x2").ToLower());
                }
                return sb.ToString();
            }
        }
    }
}
