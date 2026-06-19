using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    // Tune iterations based on your server performance; start higher than 10k.
    // (Modern guidance is higher; OWASP suggests 600k+ for PBKDF2-HMAC-SHA256, but SHA1 variant differs.) [1](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
    public const int DefaultIterations = 100_000;
    public const int SaltSize = 16;      // bytes
    public const int KeySize = 32;       // bytes (256-bit)


    public static (byte[] Hash, byte[] Salt, int Iterations, byte AlgoVersion) HashPassword(string password)
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(salt);

        byte[] hash;
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DefaultIterations))
            hash = pbkdf2.GetBytes(KeySize);

        return (Hash: hash, Salt: salt, Iterations: DefaultIterations, AlgoVersion: 1);
    }


    public static bool VerifyPassword(string password, byte[] salt, int iterations, byte[] expectedHash)
    {
        byte[] actualHash;
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            actualHash = pbkdf2.GetBytes(expectedHash.Length);
        }

        return FixedTimeEquals(actualHash, expectedHash);
    }

    // Constant-time compare to reduce timing attack leakage
    private static bool FixedTimeEquals(byte[] a, byte[] b)
    {
        if (a == null || b == null || a.Length != b.Length) return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
            diff |= a[i] ^ b[i];

        return diff == 0;
    }
}