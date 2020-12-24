using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SecurityProject.Constants;
using System.IO;

namespace SecurityProject.Services
{
    public class AESService : IAESService
    {
        public PasswordEncryptionResult EncryptPassword(string password)
        {
            using (AesManaged aes = new AesManaged())
            {
                byte[] encrypted;
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(password);
                        encrypted = ms.ToArray();
                    }
                }
                return new PasswordEncryptionResult
                {
                    IV = aes.IV,
                    Key = aes.Key,
                    Encryption = encrypted
                };
            }
        }
        public string DecryptPassword(byte[] password, byte[] key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(password))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        public bool IsPasswordMatch(string password, byte[] key, byte[] IV, byte[] encryption) => DecryptPassword(encryption, key, IV) == password;
    }
}
