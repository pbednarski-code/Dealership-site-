using System.Security.Cryptography;
using System.Text;

namespace DealerAutoMVC.Helpers
{
    public static class HashHelper
    {
        public static string ObliczHash(string tekst)
        {
            Encoding enc = Encoding.UTF8;
            StringBuilder hashBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                byte[] result = hash.ComputeHash(enc.GetBytes(tekst));

                foreach (byte b in result)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }
            }

            return hashBuilder.ToString();
        }
    }
}