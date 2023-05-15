using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Utilities
{
    public static class EncryptionDencryption
    {
        public static string key = "F!ReMaN@$$_Encrypt";
        public static string EncryptPass(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string DecryptPass(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;

        }
    }
}
