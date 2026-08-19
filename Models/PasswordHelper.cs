using System.Security.Cryptography;
using System.Text;

namespace tp05.Models;

public static class PasswordHelper
{
    public static (string hash, string salt) CrearHash(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
        string salt = Convert.ToBase64String(saltBytes);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            100_000,
            HashAlgorithmName.SHA256);

        byte[] hashBytes = pbkdf2.GetBytes(32);
        string hash = Convert.ToBase64String(hashBytes);

        return (hash, salt);
    }

    public static bool VerificarPassword(string password, string hashGuardado, string saltGuardado)
    {
        byte[] saltBytes = Convert.FromBase64String(saltGuardado);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            100_000,
            HashAlgorithmName.SHA256);

        byte[] hashCalculado = pbkdf2.GetBytes(32);
        string hashActual = Convert.ToBase64String(hashCalculado);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(hashActual),
            Encoding.UTF8.GetBytes(hashGuardado));
    }
}