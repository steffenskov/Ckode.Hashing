using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ckode.Hashing.Configurations;
using Konscious.Security.Cryptography;

namespace Ckode.Hashing
{
	public class Argon2 : IHashingAlgorithm
	{
        public Argon2Configuration Configuration { get; } = new Argon2Configuration();
		
        // The following constants must not be changed!
		private const int ITERATION_INDEX = 0;
        private const int DEGREE_OF_PARALLELISM_INDEX = 1;
		private const int MEMORY_SIZE_INDEX = 2;
		private const int SALT_INDEX = 3;
		private const int HASH_INDEX = 4;

		/// <summary>
		/// Creates a salted Argon2 hash of the input.
		/// </summary>
		/// <param name="input">The input to hash.</param>
		/// <returns>The hash of the input.</returns>
		public string CreateHash(string input)
		{
			byte[] salt;
			// Generate a random salt
			using (var csprng = new RNGCryptoServiceProvider())
			{
				salt = new byte[Configuration.SaltSize];
				csprng.GetBytes(salt);
			}

			// Hash the input and encode the parameters
			var hash = PerformHashing(input, salt, Configuration.Iterations, Configuration.DegreeOfParallelism, Configuration.MemorySize, Configuration.HashSize);
			return $"{Configuration.Iterations}:{Configuration.DegreeOfParallelism}:{Configuration.MemorySize}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
		}

		public bool IsThisAlgorithm(string correctHash)
		{
			return correctHash?.Split(':').Length == 5;
		}

		public bool ValidateHash(string input, string correctHash)
		{
			// Extract the parameters from the hash
			char[] delimiter = { ':' };
			var split = correctHash.Split(delimiter);
			var iterations = Int32.Parse(split[ITERATION_INDEX]);
			var degreeOfParallelism = Int32.Parse(split[DEGREE_OF_PARALLELISM_INDEX]);
			var memorySize = Int32.Parse(split[MEMORY_SIZE_INDEX]);
			var salt = Convert.FromBase64String(split[SALT_INDEX]);
			var hash = Convert.FromBase64String(split[HASH_INDEX]);

			var testHash = PerformHashing(input, salt, iterations, degreeOfParallelism, memorySize, hash.Length);
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
				diff |= (uint)(a[i] ^ b[i]);
			return diff == 0;
		}

		/// <summary>
		/// Computes the Argon2 hash of an input.
		/// </summary>
		/// <param name="input">The input to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The iteration count.</param>
		/// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the input.</returns>
		private static byte[] PerformHashing(string input, byte[] salt, int iterations, int degreeOfParallelism, int memorySize, int outputBytes)
		{
			var inputBytes = Encoding.Unicode.GetBytes(input);
			using (var argon = new Argon2i(inputBytes)
			{
				DegreeOfParallelism = degreeOfParallelism,
				Iterations = iterations,
				MemorySize = memorySize,
				Salt = salt
			})
			{
                return Task.Run(async () => await argon.GetBytesAsync(outputBytes)).Result;
                    //AsyncHelpers.RunSync(() => argon.GetBytesAsync(outputBytes)); // Messy workaround for poorly implemented "GetBytes" in the Argon lib (the implementation in there can and will deadlock)
			}
		}
    }
}
