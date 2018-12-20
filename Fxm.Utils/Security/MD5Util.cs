using System.Security.Cryptography;
using System.Text;

namespace Fxm.Utils.Security
{
    public class MD5Util
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="bit">md5位数，16位|32位，默认用32位</param>
        /// <returns></returns>
        public static string Encrypt(string data, MD5Bit bit = MD5Bit.Bit32)
        {
            return Encrypt(data, new UTF8Encoding());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <param name="bit">md5位数，16位|32位，默认用32位</param>
        /// <returns></returns>
        public static string Encrypt(string data, Encoding encode, MD5Bit bit = MD5Bit.Bit32)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] hash;
            if (bit == MD5Bit.Bit16)
            {
                hash = md5.ComputeHash(encode.GetBytes(data), 4, 8);
            }
            else hash = md5.ComputeHash(encode.GetBytes(data));

            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x").PadLeft(2, '0'));

            return sb.ToString();
        }
    }

    /// <summary>
    /// MD5的位数
    /// </summary>
    public enum MD5Bit
    {
        Bit16,
        Bit32
    }
}
