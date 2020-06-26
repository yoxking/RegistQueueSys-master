using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace EntFrm.Framework.Utility
{
    public class EnconfigHelper
    {
        /// <summary> 
        /// 加密 
        /// </summary> 
        /// <param name="strText"></param> 
        /// <returns></returns> 
        public static string Encrypt(string strText)
        { 
            Byte[] rgbKey = { 2, 22, 42, 62, 82, 102, 122, 142 };
            Byte[] rgbIV = { 3, 9, 22, 32, 42, 51, 61, 71 };

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary> 
        /// 解密 
        /// </summary> 
        /// <param name="strText"></param> 
        /// <returns></returns> 
        public static string Decrypt(string strText)
        {
            Byte[] rgbKey = { 2, 22, 42, 62, 82, 102, 122, 142 };
            Byte[] rgbIV = { 3, 9, 22, 32, 42, 51, 61, 71 };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    } 
}
