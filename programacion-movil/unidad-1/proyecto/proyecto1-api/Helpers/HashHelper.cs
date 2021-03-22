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
            var sha = SHA256.Create();
            byte[] codificar = Encoding.UTF8.GetBytes(contra);
            byte[] hash = sha.ComputeHash(codificar);
            string a = "";
            foreach (var objeto in hash)
            {
                a += objeto.ToString("x2");
            }
            return a;
        }
    }
}