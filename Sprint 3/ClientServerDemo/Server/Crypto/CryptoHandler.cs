using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Server.Crypto
{
    public static class CryptoHandler
    {
        public static byte[] EncryptContent(string content)
        {
            byte[] encrypted;
            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.Padding = PaddingMode.PKCS7;
                aesManaged.KeySize = 128;
                aesManaged.Key = new byte[128 / 8];
                aesManaged.IV = new byte[128 / 8];

                ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(content);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string DecryptContent(byte[] content)
        {
            string plaintext = null;
            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.Padding = PaddingMode.PKCS7;
                aesManaged.KeySize = 128;
                aesManaged.Key = new byte[128 / 8];
                aesManaged.IV = new byte[128 / 8];

                ICryptoTransform decryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);
                using (MemoryStream ms = new MemoryStream(content))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plaintext = sr.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}