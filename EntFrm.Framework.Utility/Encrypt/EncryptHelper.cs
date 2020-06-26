using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EntFrm.Framework.Utility
{
      public enum encrytpType
    {
        None = 0,
        MD5 = 1,
        Rijndael,
        RC2,
        DES,
        DES3_CBC,
        DES3_ECB
    }

    /// <summary>
    /// 加密字符串辅助类
    /// </summary>
    public class EncryptHelper
    { 
        public static string EncryptData(string sMessage, encrytpType enType, string sEnptKey, string sEnptIV, int iKeySize)
        {
            try
            {
                byte[] bMessage = Encoding.UTF8.GetBytes(sMessage);
                byte[] bResult = null;
                string strResult = null;
                byte[] bKey = Convert.FromBase64String(sEnptKey);
                byte[] bIV = Convert.FromBase64String(sEnptIV);

                switch (enType)
                {
                    case encrytpType.None:
                        bResult = bMessage;
                        break;
                    case encrytpType.MD5:
                        bResult = MD5hash(bMessage, iKeySize);
                        break;
                    case encrytpType.Rijndael:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = Rijndael(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.RC2:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = RC2Crypto(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = DESCrypto(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES3_CBC:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = Des3EncodeCBC(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES3_ECB:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = Des3EncodeECB(bMessage, bKey, bIV, iKeySize);
                        break;
                    default:
                        bResult = null;
                        break;
                }

                strResult = Convert.ToBase64String(bResult);

                return strResult;
            }
            catch (Exception ex)
            {
                throw new Exception(" 加密信息时出错;" + ex.Message);
            }
        }

        public static string GenerateKey(encrytpType enType)
        {
            try
            { 
                byte[] bResult = null;
                string strResult = null; 

                switch (enType)
                { 
                    case encrytpType.Rijndael:
                        RijndaelManaged rjCrypto = (RijndaelManaged)RijndaelManaged.Create();
                        bResult = rjCrypto.Key;
                        break;
                    case encrytpType.RC2:
                        RC2CryptoServiceProvider rcCrypto = (RC2CryptoServiceProvider)RC2CryptoServiceProvider.Create();
                        bResult = rcCrypto.Key;
                        break;
                    case encrytpType.DES:
                        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
                        bResult = desCrypto.Key;
                        break;
                    case encrytpType.DES3_ECB:
                        TripleDESCryptoServiceProvider ecbCrypto = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();
                        bResult = ecbCrypto.Key;
                        break; 
                    default:
                        TripleDESCryptoServiceProvider des3Crypto = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();
                        bResult = des3Crypto.Key;
                        break; 
                }

                strResult = Convert.ToBase64String(bResult);

                return strResult;
            }
            catch (Exception ex)
            {
                throw new Exception(" 生成Key时出错;" + ex.Message);
            }
        }

        public static string GenerateIV(encrytpType enType)
        {
            try
            {
                byte[] bResult = null;
                string strResult = null;

                switch (enType)
                {
                    case encrytpType.Rijndael:
                        RijndaelManaged rjCrypto = (RijndaelManaged)RijndaelManaged.Create();
                        bResult = rjCrypto.IV;
                        break;
                    case encrytpType.RC2:
                        RC2CryptoServiceProvider rcCrypto = (RC2CryptoServiceProvider)RC2CryptoServiceProvider.Create();
                        bResult = rcCrypto.IV;
                        break;
                    case encrytpType.DES:
                        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
                        bResult = desCrypto.IV;
                        break;
                    case encrytpType.DES3_ECB:
                        TripleDESCryptoServiceProvider ecbCrypto = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();
                        bResult = ecbCrypto.IV;
                        break;
                    default:
                        TripleDESCryptoServiceProvider des3Crypto = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();
                        bResult = des3Crypto.IV;
                        break;
                }

                strResult = Convert.ToBase64String(bResult);

                return strResult;
            }
            catch (Exception ex)
            {
                throw new Exception(" 生成IV时出错;" + ex.Message);
            }
        }

        private static byte[] MD5hash(byte[] bData, int iKeySize)
        {
            try
            {
                // This is one implementation of the abstract class MD5.
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] result = md5.ComputeHash(bData);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用MD5算法加密信息时出错;" + ex.Message);
            }
        }        

        private static byte[] Rijndael(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                RijndaelManaged myRijndael = new RijndaelManaged();

                myRijndael.KeySize = iKeySize;

                byte[] encrypted;
                byte[] toEncrypt = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                //Get an encryptor.
                ICryptoTransform encryptor = myRijndael.CreateEncryptor(key, IV);

                //Encrypt the data.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                //Write all data to the crypto stream and flush it.
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                //Get encrypted array of bytes.
                encrypted = msEncrypt.ToArray();

                msEncrypt.Close();
                csEncrypt.Close();

                return encrypted;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用Rijndael算法加密信息时出错;" + ex.Message);
            }

        }

        private static byte[] RC2Crypto(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

                rc2CSP.KeySize = iKeySize;

                byte[] encrypted;
                byte[] toEncrypt = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                //Get an encryptor.
                ICryptoTransform encryptor = rc2CSP.CreateEncryptor(key, IV);

                //Encrypt the data.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                //Write all data to the crypto stream and flush it.
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                //Get encrypted array of bytes.
                encrypted = msEncrypt.ToArray();

                msEncrypt.Close();
                csEncrypt.Close();

                return encrypted;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用RC2算法加密信息时出错;" + ex.Message);
            }
        }

        private static byte[] DESCrypto(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                DESCryptoServiceProvider myDES = new DESCryptoServiceProvider();

                myDES.KeySize = iKeySize;

                byte[] encrypted;
                byte[] toEncrypt = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                //Get an encryptor.
                ICryptoTransform encryptor = myDES.CreateEncryptor(key, IV);

                //Encrypt the data.
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                //Write all data to the crypto stream and flush it.
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                //Get encrypted array of bytes.
                encrypted = msEncrypt.ToArray();

                msEncrypt.Close();
                csEncrypt.Close();

                return encrypted;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用DES算法加密信息时出错;" + ex.Message);
            }
        }

        public static string DecryptData(string sMessage, encrytpType enType, string sEnptKey, string sEnptIV, int iKeySize)
        {
            try
            {
                byte[] bMessage = Convert.FromBase64String(sMessage);
                byte[] bKey = Convert.FromBase64String(sEnptKey);
                byte[] bIV = Convert.FromBase64String(sEnptIV);

                //bKey[0] = (byte)GetRandomValue();
                //bIV[0] = (byte)GetRandomValue();

                string sResult = null;
                byte[] bResult;

                switch (enType)
                {
                    case encrytpType.None:
                        bResult = bMessage;
                        break;
                    case encrytpType.MD5:
                        bResult = bMessage;
                        break;
                    case encrytpType.Rijndael:
                        bResult = DeRijndael(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.RC2:
                        bResult = DeRC2Crypto(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES:
                        bResult = DeDESCrypto(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES3_CBC:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = Des3DecodeCBC(bMessage, bKey, bIV, iKeySize);
                        break;
                    case encrytpType.DES3_ECB:
                        //bKey[0] = (byte)GetRandomValue();
                        //bIV[0] = (byte)GetRandomValue();
                        bResult = Des3DecodeECB(bMessage, bKey, bIV, iKeySize);
                        break;
                    default:
                        bResult = bMessage;
                        break;
                }

                sResult = Encoding.UTF8.GetString(bResult);

                return sResult;
            }
            catch (Exception ex)
            {
                throw new Exception(" 解密信息时出错;" + ex.Message);
            }
        }

        private static byte[] DeMD5hash(byte[] bData, int iKeySize)
        {
            try
            {
                // This is one implementation of the abstract class MD5.
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] result = md5.ComputeHash(bData);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用MD5算法解密信息时出错;" + ex.Message);
            }
        }

        private static byte[] DeRijndael(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                RijndaelManaged myRijndael = new RijndaelManaged();

                myRijndael.KeySize = iKeySize;

                byte[] encrypted = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                ICryptoTransform encryptor = myRijndael.CreateDecryptor(key, IV);

                // Create a memory stream to the passed buffer.
                MemoryStream msDesrypt = new MemoryStream(encrypted);

                // Create a CryptoStream using the memory stream and the 
                CryptoStream csDesrypt = new CryptoStream(msDesrypt, encryptor, CryptoStreamMode.Read);

                byte[] result = new byte[encrypted.Length];

                csDesrypt.Read(result, 0, (int)result.Length);

                msDesrypt.Close();
                csDesrypt.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用Rijndael算法解密信息时出错;" + ex.Message);
            }
        }

        private static byte[] DeRC2Crypto(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

                rc2CSP.KeySize = iKeySize;

                byte[] encrypted = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                ICryptoTransform encryptor = rc2CSP.CreateDecryptor(key, IV);

                // Create a memory stream to the passed buffer.
                MemoryStream msDesrypt = new MemoryStream(encrypted);

                // Create a CryptoStream using the memory stream and the 
                CryptoStream csDesrypt = new CryptoStream(msDesrypt, encryptor, CryptoStreamMode.Read);

                byte[] result = new byte[encrypted.Length];

                csDesrypt.Read(result, 0, (int)result.Length);

                msDesrypt.Close();
                csDesrypt.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用RC2算法解密信息时出错;" + ex.Message);
            }
        }

        private static byte[] DeDESCrypto(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                DESCryptoServiceProvider myDES = new DESCryptoServiceProvider();

                myDES.KeySize = iKeySize;

                byte[] encrypted = bData;
                byte[] key = bKey;
                byte[] IV = bIV;

                ICryptoTransform encryptor = myDES.CreateDecryptor(key, IV);

                // Create a memory stream to the passed buffer.
                MemoryStream msDesrypt = new MemoryStream(encrypted);

                // Create a CryptoStream using the memory stream and the 
                CryptoStream csDesrypt = new CryptoStream(msDesrypt, encryptor, CryptoStreamMode.Read);

                byte[] result = new byte[encrypted.Length];

                csDesrypt.Read(result, 0, (int)result.Length);

                csDesrypt.Close();
                msDesrypt.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(" 使用DES算法解密信息时出错;" + ex.Message);
            }
        }

        public static byte[] StringToBytes(string strSource, char cSeparator)
        {
            try
            {
                string[] strMessage = strSource.Split(cSeparator);

                byte[] bMessage = new byte[strMessage.GetLength(0)];

                for (int i = 0; i < bMessage.Length; i++)
                {
                    bMessage[i] = byte.Parse(strMessage[i]);
                }

                return bMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(" 将字符串值转换为字节值时出错;" + ex.Message);
            }
        }

        public static string BytesToString(byte[] bSource, char cSeparator)
        {
            try
            {
                string strResult = null;

                for (int i = 0; i < bSource.Length; i++)
                {
                    if (i == (bSource.Length - 1))
                        strResult += bSource[i].ToString();
                    else
                        strResult += bSource[i].ToString() + cSeparator.ToString();
                }

                return strResult;
            }
            catch (Exception ex)
            {
                throw new Exception(" 将字节值转换字符串值时出错;" + ex.Message);
            }
        }

        private static int GetRandomValue()
        {
            try
            {
                int ResultValue = (DateTime.Now.Year % 97) * 1000 + (DateTime.Now.Month % 11) * 100 + DateTime.Now.Hour;
                ResultValue = ResultValue % 247;

                return ResultValue;
            }
            catch (Exception ex)
            {
                throw new Exception(" 产生随机数值时出错;" + ex.Message);
            }
        }

        #region CBC模式**

        /// <summary>
        /// DES3 CBC模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        private static byte[] Des3EncodeCBC(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            //复制于MSDN

            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;             //默认值
                tdsp.Padding = PaddingMode.PKCS7;       //默认值
                tdsp.KeySize = iKeySize;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(bData, 0, bData.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// DES3 CBC模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV</param>
        /// <param name="data">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        private static byte[] Des3DecodeCBC(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(bData);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.CBC;
                tdsp.Padding = PaddingMode.PKCS7;
                tdsp.KeySize = iKeySize;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(bKey, bIV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[bData.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        #endregion

        #region ECB模式

        /// <summary>
        /// DES3 ECB模式加密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">明文的byte数组</param>
        /// <returns>密文的byte数组</returns>
        private static byte[] Des3EncodeECB(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                tdsp.KeySize = iKeySize;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    tdsp.CreateEncryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(bData, 0, bData.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the 
                // MemoryStream that holds the 
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                throw e;
            }

        }

        /// <summary>
        /// DES3 ECB模式解密
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">IV(当模式为ECB时，IV无用)</param>
        /// <param name="str">密文的byte数组</param>
        /// <returns>明文的byte数组</returns>
        private static byte[] Des3DecodeECB(byte[] bData, byte[] bKey, byte[] bIV, int iKeySize)
        {
            try
            {
                // Create a new MemoryStream using the passed 
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(bData);

                TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
                tdsp.Mode = CipherMode.ECB;
                tdsp.Padding = PaddingMode.PKCS7;
                tdsp.KeySize = iKeySize;

                // Create a CryptoStream using the MemoryStream 
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    tdsp.CreateDecryptor(bKey, bIV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[bData.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        #endregion
    }
}
