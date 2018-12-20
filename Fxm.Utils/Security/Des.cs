using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fxm.Utils.Security
{
    /// <summary>
    /// DES 加密、解密
    /// </summary>
    public class Des
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密数据</param>
        /// <param name="key">8位的密钥字符串</param>
        /// <param name="iv">8位的偏移量</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, byte[] iv)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, iv), CryptoStreamMode.Write))
                {
                    //若要设置字符编码，可用此函数的重载
                    using (StreamWriter sw = new StreamWriter(cst))
                    {
                        sw.Write(data);
                        sw.Flush();
                        cst.FlushFinalBlock();
                        sw.Flush();
                        return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                    }
                }
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">8位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, byte[] iv)
        {
            byte[] byKey = Encoding.ASCII.GetBytes(key);

            //先将数据base64解密，以获取原始的数据
            byte[] byEnc = Convert.FromBase64String(data);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream(byEnc))
            {
                using (CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, iv), CryptoStreamMode.Read))
                {
                    //若要设置字符编码，可用此函数的重载
                    using (StreamReader sr = new StreamReader(cst))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
