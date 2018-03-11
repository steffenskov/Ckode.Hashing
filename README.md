# Ckode.Hashing
A collection of simplified wrappers around the most commonly used hashing algorithms.

Currently supports the following algorithms:
- MD5
- SHA256
- PBKDF2
- Argon2

The first 3 algorithms are from the .NET framework, whereas Argon2 is wrapping the great work of kmaragon which can be found here: https://github.com/kmaragon/Konscious.Security.Cryptography

All algorithms share a common interface which exposes just 3 methods:
- string CreateHash(string input);
- bool ValidateHash(string input, string correctHash);
- bool IsThisAlgorithm(string correctHash);

The hashes created are converted to Base64 strings and merged with the necessary data to verify them in the future (salt, iterations, and so forth).
PBKDF2 and Argon2 automatically generate a random salt, whereas MD5 and SHA256 doesn't use salts at all. (They shouldn't be used for passwords anyway, as they're far too easy to brute force)

If you want to hash passwords I strongly recommend Argon2 as it's the most brute force resilient hashing algorithm at the moment of this writing (early 2018)

Also be sure to look at the Ckode.Hashing.Examples project, to see how the different algorithms work.
