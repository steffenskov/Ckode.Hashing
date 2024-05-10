using System;
using System.Security.Cryptography;
using System.Text;

namespace Ckode.Hashing
{
	/// <summary>
	/// Contains SHA256 functions.
	/// </summary>
	public class SHA256 : BaseHashAlgorithm<SHA256CryptoServiceProvider>, IHashingAlgorithm
	{
		public SHA256() : base(() => new SHA256CryptoServiceProvider())
		{
		}
	}
}
