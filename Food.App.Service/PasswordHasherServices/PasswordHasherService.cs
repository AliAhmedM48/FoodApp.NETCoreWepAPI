using System.Security.Cryptography;

namespace Food.App.Service.PasswordHasherServices
{
    public static class PasswordHasherService
    {
        public static String HashPassord(string password)
        {
            var salt = GenerateSalt();
            var hashedPassword = HashPasswordWithSalt(password, salt);
            return $"{salt}:{hashedPassword}";

        }

        public static bool ValidatePassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = parts[0];
            var hash = parts[1];

            var computedHash = HashPasswordWithSalt(password, salt);

            return hash.SequenceEqual(computedHash);

        }

        private static string GenerateSalt()
        {
            var saltPassword = new byte[16];
            using(var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltPassword);
            }
            return Convert.ToBase64String(saltPassword);
        }

        private static string HashPasswordWithSalt(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                return Convert.ToBase64String(pbkdf2.GetBytes(32)); // 32-byte hash
            }
        }
    }
}
