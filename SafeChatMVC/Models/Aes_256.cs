using System.Security.Cryptography;
using System.Text;


namespace SafeChatMVC.Models
{
    public class Aes_256
    {
        private static byte[] DeriveKeyFromPassword(string password)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16; // 16 bytes equal 128 bits.
            var hashMethod = HashAlgorithmName.SHA384;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }
        private static byte[] IV =
{
    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
};
        public static string Encrypt(string plainText, string key )
        {
            byte[] iv = IV;
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKeyFromPassword(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string cipherText, string key)
        {
            byte[] iv = IV;
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = DeriveKeyFromPassword(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
   
        //-------------------------
        //private static byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        //private static int BlockSize = 128;
        //public static string Encrypt(string text, string pass)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(text);
        //    //Encrypt
        //    SymmetricAlgorithm crypt = Aes.Create();
        //    HashAlgorithm hash = MD5.Create();
        //    crypt.BlockSize = BlockSize;
        //    crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(pass));
        //    crypt.IV = IV;

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        using (CryptoStream cryptoStream =
        //           new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
        //        {
        //            cryptoStream.Write(bytes, 0, bytes.Length);
        //        }

        //        return (Convert.ToBase64String(memoryStream.ToArray()));
        //    }
        //}
        //public static string Decrypt(string text, string pass)
        //{
        //    //Decrypt
        //    byte[] bytes = Convert.FromBase64String(text);
        //    SymmetricAlgorithm crypt = Aes.Create();
        //    HashAlgorithm hash = MD5.Create();
        //    crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(pass));
        //    crypt.IV = IV;

        //    using (MemoryStream memoryStream = new MemoryStream(bytes))
        //    {
        //        using (CryptoStream cryptoStream =
        //           new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
        //        {
        //            byte[] decryptedBytes = new byte[bytes.Length];
        //            cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
        //            return (Encoding.Unicode.GetString(decryptedBytes).Replace("\0\0\0\0\0\0\0\0", ""));
        //        }
        //    }
        //}
    }


