using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Pubutils
{
    public class IEncryptHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">加密前明文内容</param>
        /// <param name="Key">16位密钥<</param>
        /// <returns></returns>
        public static String Encrypt_AES(String str, String Key)
        {
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(Key);
            Byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Str">密文内容</param>
        /// <param name="Key">16位密钥<</param>
        /// <returns></returns>
        public static String Decrypt_AES(String Str, String Key)
        {
            Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(Key);
            Byte[] toEncryptArray = Convert.FromBase64String(Str);

            System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
            rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return System.Text.UTF8Encoding.UTF8.GetString(resultArray);
        }


    }
}
