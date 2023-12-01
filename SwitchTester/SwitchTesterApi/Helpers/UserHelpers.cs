using System.Security.Cryptography;
using System.Text.RegularExpressions;

internal static partial class UserHelpers
{
    public static void CheckIfPasswordCorrect(string enteredPassword, byte[] storedSalt, byte[] storedPasswordHash)
    {
        var enteredPassHash = GeneratePasswordHash(enteredPassword, storedSalt);

        if (!storedPasswordHash.SequenceEqual(enteredPassHash))
            throw new ArgumentException("The password assigned to perform the operation is incorrect.", nameof(enteredPassword));
    }

    public static void CheckPasswordFormat(string userPassword)
    {
        // This expression requires at least one lowercase letter, 
        // one uppercase letter, one number, and one special character, 
        // and must be at least 8 characters long

        if (!UserPasswordRegex().IsMatch(userPassword))
            throw new ArgumentException("The password does not comply with established policies", nameof(userPassword));
    }

    public static void CheckUserNameFormat(string userName)
    {
        // The username only contains lowercase letters, uppercase letters, 
        // numbers and the special character _, and has a minimum length of 5 
        // and a maximum of 30

        if (!UserNameRegex().IsMatch(userName))
            throw new ArgumentException("The username does not comply with established policies", nameof(userName));
    }

    public static byte[] GeneratePasswordHash(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);

        return pbkdf2.GetBytes(32);
    }

    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }

    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$")]
    private static partial Regex UserPasswordRegex();
    
    [GeneratedRegex("^[a-zA-Z0-9_]{5,30}$")]
    private static partial Regex UserNameRegex();
}