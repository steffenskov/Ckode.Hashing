namespace Ckode.Hashing.Configurations
{
    /// <summary>
    /// Configuration settings for the difficulty of hashing using PBKDF2.
    /// The default values have been tested on a Ryzen 1700 @ 3,7ghz.
    /// Here it takes aproxximately 600ms to hash a value.
    /// </summary>
    public class PBKDF2Configuration
    {
        /// <summary>
        /// How large the salt should be, measured in bytes.
        /// </summary>
        public int SaltSize { get; set; } = 24;
        /// <summary>
        /// How large the resulting hash should be, measured in bytes.
        /// </summary>
        public int HashSize { get; set; } = 24;

        /// <summary>
        /// Number of iterations to run the hash over. PBKDF2 requires a very high amount of iterations to stay secure.
        /// </summary>
        public int Iterations { get; set; } = 1 << 16;
    }
}
