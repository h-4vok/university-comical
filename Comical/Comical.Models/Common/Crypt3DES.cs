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

        private Crypto3DES() => this.Setup();
        static Crypto3DES() { }

        public static Crypto3DES obj { get; } = new Crypto3DES();

        #endregion

        private readonly byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        private readonly byte[] iv = new byte[] { 8, 7, 6, 5, 4, 3, 2, 1 };

        private readonly TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
        private ICryptoTransform encryptor;
        private ICryptoTransform decryptor;
        private readonly UTF8Encoding encoder = new UTF8Encoding();

        private void Setup()
        {
            this.encryptor = provider.CreateEncryptor(this.key, this.iv);
            this.decryptor = provider.CreateDecryptor(this.key, this.iv);
        }

        public string GetChecksum(IEnumerable<string> data)
        {
            var joinedData = String.Join("--,--", data);

            var input = this.encoder.GetBytes(joinedData);

            var byteOutput = this.GetChecksum(input);

            var output = this.encoder.GetString(byteOutput);

            return output;
        }

        protected byte[] GetChecksum(byte[] data)
        {
            using (var stream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(stream, this.encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                cryptoStream.Position = 0;

                var result = new byte[stream.Length - 1];

                stream.Read(result, 0, result.Length);

                return result;
            }
        }
    }
}
