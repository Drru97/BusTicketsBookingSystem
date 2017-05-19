using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.DataAccess.Helpers
{
    public static class Hashable
    {
        /// <summary>
        /// Provides hashing via SHA1
        /// </summary>
        /// <param name="password">Input string to hash</param>
        /// <returns>SHA1 hash of input string</returns>
        public static string Hash(string password)
        {
            using (var hasher = new SHA1Managed())
            {
                var hash = hasher.ComputeHash(Encoding.Unicode.GetBytes(password));
                var stringBuilder = new StringBuilder(2 * hash.Length);

                foreach (var b in hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
