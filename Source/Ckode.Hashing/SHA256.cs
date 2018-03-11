using System.Security.Cryptography;
using System.Text;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains SHA256 functions.
	/// </summary>
	public class SHA256 : IHashingAlgorithm
	{
		public string CreateHash(string input)
		{
			return HashBytes(Encoding.Unicode.GetBytes(input));
		}

		public bool IsThisAlgorithm(string correctHash)
		{
			return correctHash?.Length == 64;
		}

		public bool ValidateHash(string input, string correctHash)
		{
			return CreateHash(input) == correctHash;
		}

		private string HashBytes(byte[] bytes)
		{
			using (var hasher = new SHA256CryptoServiceProvider())
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
