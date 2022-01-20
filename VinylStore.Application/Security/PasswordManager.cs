using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace VinylStore.Application.Security;

public class PasswordManager
{
    public static (string Salt, string Hash) GenerateSaltedHash(string password)
    {
        var salt = new byte[128 / 8];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetNonZeroBytes(salt);
        }
        
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        return (Convert.ToBase64String(salt), hash);
    }

    public static bool ValidatePassword(string password, string salt, string expectedHash)
    {
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        return hash == expectedHash;
    }
}
