using System.Security.Cryptography;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains SHA1 functions
	/// </summary>
	public class SHA1 : BaseHashAlgorithm<SHA1CryptoServiceProvider>, IHashingAlgorithm
	{
		public SHA1() : base(() => new SHA1CryptoServiceProvider())
		{
		}
	}
}
