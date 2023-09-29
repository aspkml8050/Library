using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Library.App_Code.CSharp
{
    public static class LoggedEncrDecr
    {
        private static int _size=8;
            public static string decrypt(string val, string seed)
            {
                byte[] KEY_64 = Convert.FromBase64String(seed);
                byte[] IV_64 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
                if (val != string.Empty)
                {
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    byte[] buffer = Convert.FromBase64String(val);
                    MemoryStream ms = new MemoryStream(buffer);
                    CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cs);
                    return sr.ReadToEnd();
                }
                else
                    return "";
            }
        public static string encrypt(string val, byte[] seed)
        {
            byte[] KEY_64;
            KEY_64 = seed;
            byte[] IV_64 = new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
            if (val != string.Empty)
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(val);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            }
            else
                return "";
        }

        public static byte[] CreateSalt(int size=8) //we use 8 chars
        {
            // // Generate a cryptographic random number using the cryptographic service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size - 1 + 1];
            rng.GetBytes(buff);
            // // Return a Base64 string representation of the random number
            return buff;
        }

    }
}
