using System;
using Xunit;

namespace Ckode.Hashing.Tests
{
	public class MD5Tests
	{
		[Fact]
		public void CreateHash_NullInput_Throws()
		{
			// Arrange
			var algorithm = new MD5();

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => algorithm.CreateHash(null));
		}

		[Fact]
		public void CreateHash_EmptyInput_Hashes()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var hash = algorithm.CreateHash(string.Empty);

			// Assert
			Assert.NotNull(hash);
			Assert.Equal(algorithm.HashLength, hash.Length);
		}

		[Fact]
		public void CreateHash_ProperInput_Hashes()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var hash = algorithm.CreateHash("Hello world");

			// Assert
			Assert.NotNull(hash);
			Assert.Equal(algorithm.HashLength, hash.Length);
		}

		[Fact]
		public void IsThisAlgorithm_NullInput_Throws()
		{
			// Arrange
			var algorithm = new MD5();

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => algorithm.IsThisAlgorithm(null));
		}

		[Fact]
		public void IsThisAlgorithm_InvalidInput_ReturnsFalse()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var isThisAlgorithm = algorithm.IsThisAlgorithm("not an MD5 hash");

			// Assert
			Assert.False(isThisAlgorithm);
		}

		[Fact]
		public void IsThisAlgorithm_ValidInput_ReturnsTrue()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var hash = algorithm.CreateHash("Hello world");
			var isThisAlgorithm = algorithm.IsThisAlgorithm(hash);

			// Assert
			Assert.True(isThisAlgorithm);
		}

		[Fact]
		public void ValidateHash_NullInput_Throws()
		{
			// Arrange
			var algorithm = new MD5();

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => algorithm.ValidateHash(null, "not an MD5 hash"));
			Assert.Throws<ArgumentNullException>(() => algorithm.ValidateHash("Hello world", null));
		}

		[Fact]
		public void ValidateHash_InvalidCorrectHash_Throws()
		{
			// Arrange
			var algorithm = new MD5();

			// Act && Assert
			Assert.Throws<ArgumentException>(() => algorithm.ValidateHash("Hello world", "not an MD5 hash"));
		}

		[Fact]
		public void ValidateHash_NotMatchingHash_ReturnsFalse()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var helloWorldHash = algorithm.CreateHash("Hello world");
			var validHash = algorithm.ValidateHash("Definately not hello world", helloWorldHash);

			// Assert
			Assert.False(validHash);
		}

		[Fact]
		public void ValidateHash_MatchingHash_ReturnsTrue()
		{
			// Arrange
			var algorithm = new MD5();

			// Act
			var helloWorldHash = algorithm.CreateHash("Hello world");
			var validHash = algorithm.ValidateHash("Hello world", helloWorldHash);

			// Assert
			Assert.True(validHash);
		}
	}
}
