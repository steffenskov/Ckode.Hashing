using System;
using System.Security.Cryptography;
using System.Text;

namespace Ckode.Hashing
{
	public abstract class BaseHashAlgorithm<T> : IHashingAlgorithm
		where T : HashAlgorithm
	{
		private readonly Func<T> constructor;
		private readonly string _hashAlgorithmName;

		public int HashLength { get; }

		protected BaseHashAlgorithm(Func<T> constructor)
		{
			this.constructor = constructor;
			using (var instance = constructor())
			{
				_hashAlgorithmName = instance.GetType().Name;
				HashLength = instance.HashSize / 4; // / 4 because HashSize is in bits, and the algorithm is creating a string of bytes formatted by 2 chars each byte.
			}
		}

		public string CreateHash(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "input is null");
			}

			return HashBytes(Encoding.Unicode.GetBytes(input));
		}

		public bool IsThisAlgorithm(string correctHash)
		{
			if (correctHash == null)
			{
				throw new ArgumentNullException(nameof(correctHash), "correctHash is null");
			}
			return correctHash?.Length == HashLength;
		}

		public bool ValidateHash(string input, string correctHash)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "input is null");
			}

			if (correctHash == null)
			{
				throw new ArgumentNullException(nameof(correctHash), "correctHash is null");
			}

			if (!IsThisAlgorithm(correctHash))
			{
				throw new ArgumentException($"correctHash is not an {_hashAlgorithmName} hash", nameof(correctHash));
			}

			return CreateHash(input) == correctHash;
		}

		private string HashBytes(byte[] bytes)
		{
			using (var hasher = constructor())
			{
				var result = hasher.ComputeHash(bytes);
				var sb = new StringBuilder(HashLength);
				foreach (var bt in result)
				{
					sb.Append(bt.ToString("x2").ToLower());
				}
				return sb.ToString();
			}
		}
	}
}
