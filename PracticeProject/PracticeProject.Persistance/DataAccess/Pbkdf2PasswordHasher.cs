using PracticeProject.Application.BaseInfos.Security;
using System;
using System.Globalization;
using System.Security.Cryptography;

namespace PracticeProject.Persistance.DataAccess
{
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private const int HashSize = 32;
        private const int SaltSize = 16;
        private const int Iterations = 100000;

        public string Hash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null.", nameof(password));

            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] hash;
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256
                ))
            {
                hash = pbkdf2.GetBytes(HashSize);
            }

            return string.Join("$",
                "PBKDF2",
                "SHA256",
                Iterations.ToString(),
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        public bool Verify(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                string[] parts = hashedPassword.Split('$');

                if (parts.Length != 5 || parts[0] != "PBKDF2" || parts[1] != "SHA256")
                    return false;

                int iterations = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (iterations < 100000 || iterations > 1000000)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[3]);
                byte[] expectedHash = Convert.FromBase64String(parts[4]);

                if (salt.Length != SaltSize || expectedHash.Length != HashSize)
                    return false;

                byte[] actualHash;
                using (var pbkdf2 = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256
                    ))
                {
                    actualHash = pbkdf2.GetBytes(HashSize);
                }

                return FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int difference = 0;

            for (int i = 0; i < a.Length; i++)
                difference |= a[i] ^ b[i];

            return difference == 0;
        }
    }
}
