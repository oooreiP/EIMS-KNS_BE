using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;

namespace EIMS.Infrastructure.Security
{
    public class LookupCodeGenerator : ILookupCodeGenerator
    {
        // Loại bỏ: 0, O, 1, I, L để tránh nhầm lẫn khi đọc
        private static readonly char[] Chars = "ABCDEFGHJKMNPQRSTUVWXYZ23456789".ToCharArray(); 

        public string Generate(int length = 10)
        {
            var data = new byte[length];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = Chars[data[i] % Chars.Length];
            }
            return new string(result);
        }
    }
}