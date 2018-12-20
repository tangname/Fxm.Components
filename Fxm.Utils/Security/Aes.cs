using System;
using System.Security.Cryptography;
using System.Text;

namespace Fxm.Utils.Security
{
    /// <summary>
    /// ASE加密、解密
    /// </summary>
    public class Aes
    {
        ///// <summary>
        ///// 约定密钥
        ///// </summary>
        //private const string key = "SZCCBFDTSYSENKEY";

        //private static byte[] iv = { 0x12, 0x18, 0x24, 0x30, 0x36, 0x42, 0x48, 0x54, 0x60, 0x70, 0x80, 0x89, 0x90, 0xab, 0xcd, 0xef };

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="data">加密的数据</param>
        /// <param name="key">约定的密钥字符串 16位</param>
        /// <param name="iv">偏移量 16位</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, byte[] iv)
        {
            //注册字符集
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (string.IsNullOrEmpty(data)) return null;

            byte[] toEncryptArray = Encoding.GetEncoding("GB2312").GetBytes(data);

            var manager = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = iv
            };

            ICryptoTransform cTransform = manager.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="data">解密的数据</param>
        /// <param name="key">约定的密钥字符串</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, byte[] iv)
        {
            //注册字符集
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (string.IsNullOrEmpty(data)) return null;

            byte[] toEncryptArray = Convert.FromBase64String(data);

            RijndaelManaged manager = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = iv
            };

            ICryptoTransform cTransform = manager.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.GetEncoding("GB2312").GetString(resultArray);
        }
    }
}
