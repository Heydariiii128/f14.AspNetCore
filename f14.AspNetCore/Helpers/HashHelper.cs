using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace f14.AspNetCore.Helpers
{
    /// <summary>
    /// Provides helper methods for computing hash for the string data.
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Computes hash for a string data.
        /// </summary>
        /// <param name="data">The string data for hashing.</param>
        /// <param name="salt">The hash salt.</param>
        /// <returns>The computed hash for given data.</returns>
        public static string Compute(string data, byte[] salt)
        {
            var hashedBytes = KeyDerivation.Pbkdf2(data, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8);
            var hashedString = Convert.ToBase64String(hashedBytes);
            return hashedString;
        }
        /// <summary>
        /// Validates the hash and the string data.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="data">The string data.</param>
        /// <param name="salt">The hash salt.</param>
        /// <returns>True - if given data is match with given hash; False - otherwise.</returns>
        public static bool Validate(string hash, string data, byte[] salt) => string.Equals(hash, Compute(data, salt));
        /// <summary>
        /// Generates random salt.
        /// </summary>
        /// <param name="length">The salt length.</param>
        /// <returns>The salt as byte array.</returns>
        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
