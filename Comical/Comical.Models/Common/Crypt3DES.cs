using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Common
{
    public class Crypto3DES
    {
        #region Singleton

        private Crypto3DES() { }
        static Crypto3DES() { }

        public static Crypto3DES obj { get; } = new Crypto3DES();

        #endregion

        public string GetChecksum(IEnumerable<string> data)
        {
            var joinedData = String.Join("--,--", data);

            var output = this.Encrypt(joinedData, "ModelosComputacionales");

            return output;
        }

        protected string Encrypt(string source, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Encoding.UTF8.GetBytes(source);

            string encoded = Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return encoded;
        }
    }
}
