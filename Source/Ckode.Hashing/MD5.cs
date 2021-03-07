using System.Security.Cryptography;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains MD5 functions.
	/// </summary>
	public class MD5 : BaseHashAlgorithm<MD5CryptoServiceProvider>, IHashingAlgorithm
	{
		public MD5() : base(() => new MD5CryptoServiceProvider())
		{
		}
	}
}
