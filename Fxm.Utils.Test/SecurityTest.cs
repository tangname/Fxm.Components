using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xunit;

namespace Fxm.Utils.Test
{
    public class SecurityTest
    {
        [Fact]
        public void AesTest()
        {
            //16位秘钥
            string key = "ABCDEFGHIJKLMNOP";
            //16位偏移
            byte[] iv = { 0x12, 0x18, 0x24, 0x30, 0x36, 0x42, 0x48, 0x54, 0x60, 0x70, 0x80, 0x89, 0x90, 0xab, 0xcd, 0xef };

            var data = "Hello world!你好！";

            //加密
            var encryptValue=Security.Aes.Encrypt(data, key, iv);
            System.Diagnostics.Trace.WriteLine(encryptValue);

            var decryptValue = Security.Aes.Decrypt(encryptValue, key, iv);

            Assert.Equal(data, decryptValue);
        }

        [Fact]
        public void DesTest()
        {
            //8位秘钥
            string key = "ABCDEFGH";
            //8位偏移
            byte[] iv = { 0x12,  0x60, 0x70, 0x80, 0x89, 0x90, 0xab, 0xef };

            var data = "Hello world!你好！";

            //加密
            var encryptValue = Security.Des.Encrypt(data, key, iv);
            System.Diagnostics.Trace.WriteLine(encryptValue);

            var decryptValue = Security.Des.Decrypt(encryptValue, key, iv);

            Assert.Equal(data, decryptValue);
        }

        [Fact]
        public void Md5Test()
        {
            var data = "Hello world!";
            var encrypt_16_length = "190d2c85f6e0468c";
            var encrypt_32_length = "86fb269d190d2c85f6e0468ceca42a20";

            //加密
            var encryptValue = Security.MD5Util.Encrypt(data,Security.MD5Bit.Bit32);
            Assert.Equal(encrypt_32_length, encryptValue);

            encryptValue = Security.MD5Util.Encrypt(data, Security.MD5Bit.Bit16);
            Assert.Equal(encrypt_16_length, encryptValue);
        }
    }
}
