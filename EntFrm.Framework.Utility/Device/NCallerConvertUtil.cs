using System;
using System.Text;

namespace EntFrm.Framework.Utility
{
    public class NCallerConvertUtil
    {
        public static NCallerDataModel ParseData(string fmtStr)
        {
            try {
                if (!string.IsNullOrEmpty(fmtStr) && fmtStr.Length > 28)
                {
                    fmtStr = fmtStr.Substring(fmtStr.IndexOf("FF-68-"));
                    int dataLength = Convert.ToInt32(fmtStr.Substring(6, 2), 16) - 2;
                    int dataPhyaddr = Convert.ToInt32(fmtStr.Substring(15, 2), 16);
                    int dataFuncode = Convert.ToInt32(fmtStr.Substring(18, 2), 16);
                    string temp = Hex2Digital(fmtStr.Substring(21, dataLength * 3)).PadLeft(2,'0');
                    string dataParam = temp.Substring(0, temp.Length - 2);
                    string dataCode = temp.Substring(temp.Length - 2, 2);

                    return new NCallerDataModel(dataPhyaddr, dataFuncode, dataParam, dataCode);

                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public static string FormatData(NCallerDataModel myInstr)
        {
            try
            {
                string DataHeader = "FF68";
                string DataSpliter = "68";

                string DataAddress = Convert.ToString(myInstr.Address, 16).PadLeft(2, '0');
                string DataInstr = Convert.ToString(myInstr.Funcode, 16).PadLeft(2, '0');
                string DataText = "";
                if (!string.IsNullOrEmpty(myInstr.Optdata))
                {
                    DataText = "0" + string.Join("0", myInstr.Optdata.ToCharArray());
                }
                string DataLength = Convert.ToString(DataText.Length / 2 + 2, 16).PadLeft(2, '0');
                string CheckCode = GetCheckCode(FromHex(DataAddress + DataInstr + DataText)).ToString();
                string DataEnder = "16";

                string FormatStr = DataHeader + DataLength + DataLength + DataSpliter + DataAddress + DataInstr + DataText + CheckCode + DataEnder;
                FormatStr = System.Text.RegularExpressions.Regex.Replace(FormatStr, @"(\w{2})", "$1-").Trim('-');

                //Byte[] datas = Hex2Bytes(FormatStr);

                return FormatStr;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        /// <summary> 
        /// 累加校验和 
        /// </summary> 
        /// <param name="memorySpage">需要校验的数据</param> 
        /// <returns>返回校验和结果</returns> 
        private static string GetCheckCode(byte[] memorySpage)
        {
            int num = 0;
            for (int i = 0; i < memorySpage.Length; i++)
            {
                num = (num + memorySpage[i]) % 0xffff;
            }
            //实际上num 这里已经是结果了，如果只是取int 可以直接返回了 
            memorySpage = BitConverter.GetBytes(num);
            //返回累加校验和 
            Int16 ret = BitConverter.ToInt16(new byte[] { memorySpage[0], memorySpage[1] }, 0);
            return ret.ToString("X2");
        }

        private static String Hex2Digital(String hex)
        {
            byte[] data = FromHex(hex);
            StringBuilder sb = new StringBuilder();
            foreach (byte bt in data)
            {
                sb.Append(bt.ToString());
            }

            return sb.ToString();
        }

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
        private static String Hex2String(String hex)
        {
            byte[] data = FromHex(hex);
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// String to hex string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static String String2Hex(String str)
        {
            Byte[] data = Encoding.Default.GetBytes(str);
            return BitConverter.ToString(data);
        }

        /// <summary>
        /// Hex string to bytes
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static Byte[] Hex2Bytes(String hex)
        {
            return FromHex(hex);
        }

        /// <summary>
        /// Bytes to Hex String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static String Bytes2Hex(Byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        /**
        * C#实现两个十六进制字符串异或的方法1（允许这两个字符串长度不等以及包含大小写）
        * @Function:  HexStrXor
        * @Author  :  vfhky  2014.11.20  https://typecodes.com
        * @param   :  HexStr1     第一个十六进制的字符串
        * @param   :  HexStr2     第二个十六进制的字符串
        * @return  :  异或结果（一个十六进制的字符串）
        */
        private static String HexStrXor(String HexStr1, String HexStr2)
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
