using System;
using System.Security.Cryptography;
using Ckode.Hashing.Configurations;

namespace Ckode.Hashing
{
	public class PBKDF2 : IHashingAlgorithm
	{
		public PBKDF2Configuration Configuration { get; } = new PBKDF2Configuration();

		// The following constants must not be changed!
		private const int ITERATION_INDEX = 0;
		private const int SALT_INDEX = 1;
		private const int PBKDF2_INDEX = 2;

		/// <summary>
		/// Creates a salted PBKDF2 hash of the input.
		/// </summary>
		/// <param name="input">The input to hash.</param>
		/// <returns>The hash of the input.</returns>
		public string CreateHash(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "input is null");
			}
			// Generate a random salt
			byte[] salt;
			using (var csprng = new RNGCryptoServiceProvider())
			{
				salt = new byte[Configuration.SaltSize];
				csprng.GetBytes(salt);
			}

			// Hash the input and encode the parameters
			var hash = PerformHashing(input, salt, Configuration.Iterations, Configuration.HashSize);
			return $"{Configuration.Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
		}

		public bool IsThisAlgorithm(string correctHash)
		{
			if (correctHash == null)
			{
				throw new ArgumentNullException(nameof(correctHash), "correctHash is null");
			}

			return correctHash?.Split(':').Length == 3;
		}

		/// <summary>
		/// Validates an input given a hash of the correct one.
		/// </summary>
		/// <param name="input">The input to check.</param>
		/// <param name="correctHash">A hash of the correct input.</param>
		/// <returns>True if the input is correct. False otherwise.</returns>
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
				throw new ArgumentException("correctHash is not an MD5 hash", nameof(correctHash));
			}

			// Extract the parameters from the hash
			char[] delimiter = { ':' };
			var split = correctHash.Split(delimiter);
			var iterations = int.Parse(split[ITERATION_INDEX]);
			var salt = Convert.FromBase64String(split[SALT_INDEX]);
			var hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

			var testHash = PerformHashing(input, salt, iterations, hash.Length);
			return SlowEquals(hash, testHash);
		}

		/// <summary>
		/// Compares two byte arrays in length-constant time. This comparison
		/// method is used so that input hashes cannot be extracted from
		/// on-line systems using a timing attack and then attacked off-line.
		/// </summary>
		/// <param name="a">The first byte array.</param>
		/// <param name="b">The second byte array.</param>
		/// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals(byte[] a, byte[] b)
		{
			var diff = (uint)a.Length ^ (uint)b.Length;
			for (var i = 0; i < a.Length && i < b.Length; i++)
			{
				diff |= (uint)(a[i] ^ b[i]);
			}

			return diff == 0;
		}

		/// <summary>
		/// Computes the PBKDF2-SHA1 hash of an input.
		/// </summary>
		/// <param name="input">The input to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The PBKDF2 iteration count.</param>
		/// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the input.</returns>
		private static byte[] PerformHashing(string input, byte[] salt, int iterations, int outputBytes)
		{
			using (var pbkdf2 = new Rfc2898DeriveBytes(input, salt))
			{
				pbkdf2.IterationCount = iterations;
				return pbkdf2.GetBytes(outputBytes);
			}
		}
	}
}