using System.Security.Cryptography;
using System.Text.RegularExpressions;

/// <summary>
/// Helper methods for user-related operations.
/// </summary>
public static partial class UserHelpers
{
    /// <summary>
    /// Checks if the provided password complies with the established policies.
    /// </summary>
    /// <param name="userPassword">The password to be checked.</param>
    /// <exception cref="ArgumentException">Thrown if the password does not comply with established policies.</exception>

    public static void CheckPasswordFormat(string password)
    {
        // This expression requires at least one lowercase letter, 
        // one uppercase letter, one number, and one special character, 
        // and must be at least 8 characters long

        if (!UserPasswordRegex().IsMatch(password))
            throw new ArgumentException("The password does not comply with established policies: " +
                "At least one lowercase letter, one uppercase letter, " +
                "one number, and one special character, " +
                "and must be at least 8 characters long.", nameof(password));
    }

    /// <summary>
    /// Checks if the provided username complies with the established policies.
    /// </summary>
    /// <param name="userName">The username to be checked.</param>
    /// <exception cref="ArgumentException">Thrown if the username does not comply with established policies.</exception>
    public static void CheckUserNameFormat(string userName)
    {
        // The username only contains lowercase letters, uppercase letters, 
        // numbers and the special character _, and has a minimum length of 5 
        // and a maximum of 30

        if (!UserNameRegex().IsMatch(userName))
            throw new ArgumentException("The username does not comply with established policies: " +
                "The username only contains lowercase letters, uppercase letters, " +
                "numbers and the special character _, and has a minimum length of 5 " +
                "and a maximum of 30.", nameof(userName));
    }

    /// <summary>
    /// Generates a password hash using the provided password and salt.
    /// </summary>
    /// <param name="password">The password for which to generate the hash.</param>
    /// <param name="salt">The salt used in the password hashing process.</param>
    /// <returns>The generated password hash.</returns>
    public static byte[] GeneratePasswordHash(string password, byte[] salt)
    {
        ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);

        return pbkdf2.GetBytes(32);
    }

    /// <summary>
    /// Generates a random salt for password hashing.
    /// </summary>
    /// <returns>The generated salt.</returns>
    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }

    /// <summary>
    /// Checks if the entered password is correct by comparing its hash with the stored hash.
    /// </summary>
    /// <param name="enteredPassword">The entered password to be checked.</param>
    /// <param name="storedSalt">The salt associated with the stored password hash.</param>
    /// <param name="storedPasswordHash">The stored password hash to compare with the entered password.</param>
    /// <exception cref="ArgumentException">Thrown if the entered password is incorrect.</exception>
    public static void CheckIfPasswordCorrect(string password, byte[] storedSalt, byte[] storedPasswordHash)
    {
        var enteredPassHash = GeneratePasswordHash(password, storedSalt);

        if (!storedPasswordHash.SequenceEqual(enteredPassHash))
            throw new ArgumentException("The password assigned to perform the operation is incorrect.", nameof(password));
    }

    /// <summary>
    /// Regular expression for validating user passwords.
    /// This expression requires at least one lowercase letter, one uppercase letter,
    /// one number, and one special character, and must be at least 8 characters long.
    /// </summary>
    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$")]
    private static partial Regex UserPasswordRegex();

    /// <summary>
    /// Regular expression for validating usernames.
    /// The username only contains lowercase letters, uppercase letters, numbers, 
    /// and the special character '_', and has a minimum length of 5 and a maximum of 30.
    /// </summary>
    [GeneratedRegex("^[a-zA-Z0-9_]{5,30}$")]
    private static partial Regex UserNameRegex();
}