using System.Security.Cryptography;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains SHA512 functions
	/// </summary>
	public class SHA512 : BaseHashAlgorithm<SHA512CryptoServiceProvider>, IHashingAlgorithm
	{
		public SHA512() : base(() => new SHA512CryptoServiceProvider())
		{
		}
	}
}
