# Ckode.Hashing
A collection of simplified wrappers around the most commonly used hashing algorithms.

Currently supports the following algorithms:
- MD5
- SHA1
- SHA256
- SHA384
- SHA512
- PBKDF2

There's a slight difference between the algorithms though, as PBKDF2 is used for password hashing, with the rest not being suited for this.
For that reason the PBKDF2 implementation will automatically generated a cryptographically strong random salt for its hash.

All algorithms however implement interface which exposes just 3 methods:
- string CreateHash(string input);
- bool ValidateHash(string input, string correctHash);
- bool IsThisAlgorithm(string correctHash);

The hashes created are converted to Base64 strings and merged with the necessary data to verify them in the future (salt, iterations, and so forth, this is only relevant for PBKDF2).

## Installation:

I recommend using the NuGet package: https://www.nuget.org/packages/Ckode.Hashing/ however you can also simply clone the repository and use the pre-compiled binaries or compile the project yourself.
As the project is licensed under MIT you're free to use it for pretty much anything you want.

## Examples:

*Create a hash value:*

    var hasher = new MD5();
    var hashedValue = hasher.CreateHash("Hello world");
    

*Validate an existing hash value:*

    var hasher = new MD5();
    var typedInput = "Hello world";
    string existingHash = GetExistingHashFromSomewhere();
    var isValid = hasher.ValidateHash(typedInput, existingHash);
    
    
**Word of caution:**
Do not validate hashes by creating a new hash from your input, and comparing it directly to your existing hash. This will FAIL when using PBKDF2 due to the random salt.


*Finally a neat trick for validating existing hashes without knowing the algorithm:*

This one involves using a servicelocator/dependency injection tool of sorts, here I'm using my own library: https://github.com/NQbbe/Ckode.ServiceLocator.

    var serviceLocator = new ServiceLocator();
    string existingHash = GetExistingHashFromSomewhere();
    var hasher = serviceLocator.CreateInstance<IHashingAlgorithm>(hashAlgorithm => hashAlgorithim.IsThisAlgorithm(existingHash));
    var typedInput = "Hello world";
    var isValid = hasher.ValidateHash(typedInput, existingHash);
    
**Note:**
PBKDF2 does not implement the IHashingAlgorithm interface, but rather IPasswordHashingAlgorithm. The signature of the two interfaces is (currently) identical, but it may differ in the future.
Furthermore this offers a slight amount of protection against improperly using a non-password suited hashing algorithm when using dependency injection/servicelocator.
