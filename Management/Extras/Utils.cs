using System.Security.Cryptography;

namespace Management.Extras;

internal class Utils
{
    public static string HashPassword(string password)
    {
        using (var hmac = new HMACSHA256())
        {
            var salt = GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var hashBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var storedHash = new byte[20];
        Array.Copy(hashBytes, 16, storedHash, 0, 20);

        var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 10000);
        var hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hash[i] != storedHash[i])
                return false;
        }
        return true;
    }
}
