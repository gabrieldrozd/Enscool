using System.Security.Cryptography;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Core.Domain.Primitives;

namespace Modules.Management.Domain.Users;

/// <summary>
/// Represents a hashed password.
/// </summary>
public class Password : ValueObject
{
    /// <summary>
    /// Gets the hashed representation of the password.
    /// </summary>
    public string Hash { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="passwordHash">The hashed representation of the password.</param>
    private Password(string passwordHash)
    {
        Ensure.Not.NullOrEmpty(passwordHash);

        Hash = passwordHash;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="input">The plain text input.</param>
    /// <returns>A new instance of the <see cref="Password"/> class with the hashed representation of the plain text input.</returns>
    public static Password Create(string input) => new(Hasher.Hash(input));

    public static Password Instantiate(string hash) => new(hash);

    /// <summary>
    /// Verifies the plain text input against the stored hash.
    /// </summary>
    /// <param name="input">The plain text input.</param>
    /// <returns>True if the plain text input matches the stored hash; otherwise false.</returns>
    public bool Verify(string input) => Hasher.Verify(input, Hash);

    public override string ToString() => Hash;

    /// <summary>
    /// Retrieves the atomic values of the <see cref="Password"/> class.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Hash;
    }

    /// <summary>
    /// Provides methods for hashing and verifying passwords.
    /// </summary>
    private static class Hasher
    {
        private const char SegmentDelimiter = ':';
        private const int KeySize = 32;
        private const int SaltSize = 16;
        private const int Iterations = 50000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        /// <summary>
        /// Hashes a plain text input.
        /// </summary>
        /// <param name="input">The plain text input.</param>
        /// <returns>The hashed representation of the plain text input with metadata delimited by ":".</returns>
        public static string Hash(string input)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, Iterations, Algorithm, KeySize);
            return string.Join(SegmentDelimiter, Convert.ToBase64String(hash), Convert.ToBase64String(salt), Iterations, Algorithm);
        }

        /// <summary>
        /// Verifies a plain text input against a hashed password.
        /// </summary>
        /// <param name="input">The plain text input.</param>
        /// <param name="hashString">The hashed password.</param>
        /// <returns><c>true</c> if the plain text input matches the hashed password; otherwise <c>false</c>.</returns>
        public static bool Verify(string input, string hashString)
        {
            var segments = hashString.Split(SegmentDelimiter);
            var hash = Convert.FromBase64String(segments[0]);
            var salt = Convert.FromBase64String(segments[1]);
            var iterations = int.Parse(segments[2]);
            var algorithm = new HashAlgorithmName(segments[3]);
            var inputHash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, hash.Length);
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }
}