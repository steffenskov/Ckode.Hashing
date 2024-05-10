using System.Security.Cryptography;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains SHA384 functions
	/// </summary>
	public class SHA384 : BaseHashAlgorithm<SHA384CryptoServiceProvider>, IHashingAlgorithm
	{
		public SHA384() : base(() => new SHA384CryptoServiceProvider())
		{
		}
	}
}
