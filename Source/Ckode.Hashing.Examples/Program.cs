using System;

namespace Ckode.Hashing.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasher = new Argon2(); // Feel free to change Argon2 to one of the other hashers to see how they work.
            hasher.Configuration.Iterations = 1 << 16; // Changing configuration to alter difficulty.

            var rawInput = "Hello world";
            var hash = hasher.CreateHash(rawInput);

            Console.WriteLine("Hash: " + hash);

            var isValidHash = hasher.ValidateHash("Hello world", hash);
            Console.WriteLine($@"Hashed value was ""Hello foo"" ? {hasher.ValidateHash("Hello foo", hash)}");
            Console.WriteLine($@"Hashed value was ""Hello world"" ? {hasher.ValidateHash("Hello world", hash)}");
        }
    }
}
