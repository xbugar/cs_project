using System.Security.Cryptography;


namespace ExpenseManager.Ui.Database;

public static class PasswordHasher
{
    public static (byte[] hashedPassword, byte[] salt) HashPassword(string password, int iterations = 100_000)
    {
        byte[] salt = new byte[16];
        
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(salt);


        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256) ;
        byte[] hashedPassword = pbkdf2.GetBytes(32); // Generate a 256-bit hash
        return (hashedPassword, salt);
    }

    public static bool VerifyPassword(string password, byte[] salt, byte[] hashedPassword, int iterations = 100_000)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] computedHash = pbkdf2.GetBytes(32);
        return CryptographicOperations.FixedTimeEquals(computedHash, hashedPassword);
    }
}
