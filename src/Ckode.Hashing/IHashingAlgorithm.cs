namespace Ckode.Hashing
{
	/// <summary>
	/// Interface for a hashing algorithm.
	/// </summary>
	public interface IHashingAlgorithm
	{
		/// <summary>
		/// Creates a hash from the given input.
		/// </summary>
		/// <returns>The hash.</returns>
		/// <param name="input">Input to hash.</param>
		string CreateHash(string input);

		/// <summary>
		/// Validates an input against the valid hash.
		/// </summary>
		/// <returns><c>true</c>, if hash was validated, <c>false</c> otherwise.</returns>
		/// <param name="input">Input to validate.</param>
		/// <param name="correctHash">Correct hash.</param>
		bool ValidateHash(string input, string correctHash);

		/// <summary>
		/// Quick test to see if the correctHash COULD be from this algorithm.
		/// It's not a 100% test, as that's impossible with hashes.
		/// </summary>
		/// <param name="correctHash"></param>
		/// <returns></returns>
		bool IsThisAlgorithm(string correctHash);
	}
}