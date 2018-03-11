namespace Ckode.Hashing.Configurations
{
    /// <summary>
    /// Configuration settings for the difficulty of hashing using Argon2.
    /// The default values have been tested on a Ryzen 1700 @ 3,7ghz.
    /// Here it takes aproxximately 1 second to hash a value.
    /// </summary>
    public class Argon2Configuration
    {
        /// <summary>
        /// How large the salt should be, measured in bytes.
        /// </summary>
        public int SaltSize { get; set; } = 16;
        /// <summary>
        /// How large the resulting hash should be, measured in bytes.
        /// </summary>
        public int HashSize { get; set; } = 32;
        /// <summary>
        /// Number of iterations to run the hash over. Argon2 doesn't require a high amount of iterations unlike PBKDF2.
        /// </summary>
        public int Iterations { get; set; } = 1 << 3;
        /// <summary>
        /// How many threads to utilize for hashing.
        /// </summary>
        public int DegreeOfParallelism { get; set; } = 4;
        /// <summary>
        /// How much memory to utilize for hashing.
        /// </summary>
        public int MemorySize { get; set; } = 1024 * 32; // KiB
    }
}
