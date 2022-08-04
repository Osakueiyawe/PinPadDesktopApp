 // Decompiled with JetBrains decompiler
// Type: GTBSecure.Secure
// Assembly: GTBSecure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04E8F80E-79CD-41C8-B312-7A8350B6D076
// Assembly location: C:\Projects\backup\psq.dll
// Compiler-generated code is shown

using System;
using System.Security.Cryptography;
using System.Text;

namespace GTBSecure
{
    public class Secure
    {
        public Secure()
        {
           
        }

        public static string EncryptString(string Message)
        {
            string s = "8*AppdevWebService123*8";
            UTF8Encoding utF8Encoding = new UTF8Encoding();
            MD5CryptoServiceProvider cryptoServiceProvider1 = new MD5CryptoServiceProvider();
            byte[] hash = cryptoServiceProvider1.ComputeHash(utF8Encoding.GetBytes(s));
            TripleDESCryptoServiceProvider cryptoServiceProvider2 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider2.Key = hash;
            cryptoServiceProvider2.Mode = CipherMode.ECB;
            cryptoServiceProvider2.Padding = PaddingMode.PKCS7;
            byte[] bytes = utF8Encoding.GetBytes(Message);
            byte[] inArray;
            try
            {
                inArray = cryptoServiceProvider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            }
            finally
            {
                cryptoServiceProvider2.Clear();
                cryptoServiceProvider1.Clear();
            }
            return Convert.ToBase64String(inArray);
        }

        public static string DecryptString(string Message)
        {
            string s = "8*AppdevWebService123*8";
            UTF8Encoding utF8Encoding = new UTF8Encoding();
            MD5CryptoServiceProvider cryptoServiceProvider1 = new MD5CryptoServiceProvider();
            byte[] hash = cryptoServiceProvider1.ComputeHash(utF8Encoding.GetBytes(s));
            TripleDESCryptoServiceProvider cryptoServiceProvider2 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider2.Key = hash;
            cryptoServiceProvider2.Mode = CipherMode.ECB;
            cryptoServiceProvider2.Padding = PaddingMode.PKCS7;
            Message = Message.Replace(" ", "+");
            byte[] inputBuffer = Convert.FromBase64String(Message);
            byte[] bytes;
            try
            {
                bytes = cryptoServiceProvider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            finally
            {
                cryptoServiceProvider2.Clear();
                cryptoServiceProvider1.Clear();
            }
            return utF8Encoding.GetString(bytes);
        }
    }
}
