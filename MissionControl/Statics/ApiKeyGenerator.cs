using System;
using System.Security.Cryptography;
using System.Text;

namespace MissionControl.Statics
{
    public class ApiKeyGenerator
    {
        public static string CreateApiKey()
        {
            byte[] key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            return apiKey;
        }

        public static string CreateApiKey(string pass)
        {
            byte[] key = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(pass));
            string apiKey = Convert.ToBase64String(key);

            return apiKey;
        }
    }
}