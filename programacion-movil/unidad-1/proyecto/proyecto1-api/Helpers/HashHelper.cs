using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace proyecto1_api.Helpers
{
    public static class HashHelper
    {
        public static string GetHash(string contra)
        {
            SHA256Managed sha = new SHA256Managed();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(contra));
            StringBuilder s = new StringBuilder();
            foreach (byte b in bytes)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }
    }
}