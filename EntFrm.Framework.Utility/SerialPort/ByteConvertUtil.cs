using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public class ByteConvertUtil
    {
        /// <summary>
     /// Hex to byte
     /// </summary>
     /// <param name="hex"></param>
     /// <returns></returns>
        private static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                try
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                }
                catch (System.Exception)
                {
                    //Do Nothing
                }

            }
            return raw;
        }

        /// <summary>
        /// Hex string to string
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static String Hex2String(String hex)
        {
            byte[] data = FromHex(hex);
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// String to hex string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String String2Hex(String str)
        {
            Byte[] data = Encoding.Default.GetBytes(str);
            return BitConverter.ToString(data);
        }

        /// <summary>
        /// Hex string to bytes
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Byte[] Hex2Bytes(String hex)
        {
            return FromHex(hex);
        }

        /// <summary>
        /// Bytes to Hex String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static String Bytes2Hex(Byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        public static String HexStrXor(String HexStr1)
        {
            byte[] DataByte = ByteConvertUtil.Hex2Bytes(HexStr1);

            // xorResult 存放校验结果。注意：初值去首元素值！
            byte xorResult = DataByte[0];
            // 求xor校验和。注意：XOR运算从第二元素开始
            for (int i = 1; i < DataByte.Length; i++)
            {
                xorResult ^= DataByte[i];
            }

            return BitConverter.ToString(new byte[] { xorResult }, 0);
        }

        /**
   * C#实现两个十六进制字符串异或的方法1（允许这两个字符串长度不等以及包含大小写）
   * @Function:  HexStrXor
   * @Author  :  vfhky  2014.11.20  https://typecodes.com
   * @param   :  HexStr1     第一个十六进制的字符串
   * @param   :  HexStr2     第二个十六进制的字符串
   * @return  :  异或结果（一个十六进制的字符串）
   */
        public static String HexStrXor(String HexStr1, String HexStr2)
        {
            //两个十六进制字符串的长度和长度差的绝对值以及异或结果
            int iHexStr1Len = HexStr1.Length;
            int iHexStr2Len = HexStr2.Length;
            int iGap, iHexStrLenLow;
            String result = String.Empty;

            //获取这两个十六进制字符串长度的差值
            iGap = iHexStr1Len - iHexStr2Len;

            //获取这两个十六进制字符串长度最小的那一个
            iHexStrLenLow = iHexStr1Len < iHexStr2Len ? iHexStr1Len : iHexStr2Len;

            /**
             * 把这两个十六进制字符串输出到控制台
             * Console.WriteLine("HexStr1=[{0}]", HexStr1);
             * Console.WriteLine("HexStr2=[{0}]", HexStr2);
             */

            int i = 0;
            try
            {
                //先把每个十六进制字符转换成整数(0~15)，异或后再转换成十六进制字符
                for (; i < iHexStrLenLow; ++i)
                {
                    result += (Convert.ToInt32(HexStr1.Substring(i, 1), 16) ^ Convert.ToInt32(HexStr2.Substring(i, 1), 16)).ToString("X");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                /**
                 * 把错误信息输出到控制台
                 * Console.WriteLine("Exception {0} thrown.", e.GetType().FullName);
                 * Console.WriteLine("Message:{0}", e.Message);
                 */
            }

            result += iGap >= 0 ? HexStr1.Substring(i, iGap) : HexStr2.Substring(i, -iGap);
            return result;
        }

    }
}
