using System.Security.Cryptography;
using System.Text;

namespace RegisterService.Application.Util;

public static class EncryptionUtil
{
    private const string SecretKey = "/qCpYa/Ka+sk2LVGMYy9nx4sw51lAtMHIMqetPeJH34=";

    /// <summary>
    /// Encrypts a plaintext string using AES-256-CBC.
    /// </summary>
    /// <param name="plainText">The plaintext to encrypt.</param>
    /// <returns>The Base64-encoded encrypted string.</returns>
    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(SecretKey.PadRight(32, ' ')); // Ensure 32 bytes
        aes.IV = new byte[16]; // 16 bytes of zeros

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// Decrypts a Base64-encoded encrypted string using AES-256-CBC.
    /// </summary>
    /// <param name="cipherText">The encrypted string to decrypt.</param>
    /// <returns>The decrypted plaintext string.</returns>
    public static string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(SecretKey.PadRight(32, ' ')); // Ensure 32 bytes
        aes.IV = new byte[16]; // 16 bytes of zeros

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var cipherBytes = Convert.FromBase64String(cipherText);
        var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}