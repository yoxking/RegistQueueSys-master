using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace EntFrm.Framework.Utility
{
    public class CommonHelper
    {
        private static Random rnd = new Random();

        public static string Get_New8ByteGuid()
        {
            return Get_New8ByteGuid("");
        }

        /// <summary>
        /// 生成特定位数的唯一字符串
        /// </summary>
        /// <param name="num">特定位数</param>
        /// <returns></returns>
        public static string Get_UniqueStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static string Get_New8ByteGuid(string sPrefix)
        {
            try
            {
                string strNewGuid = "";
                DateTime date = DateTime.Now;

                strNewGuid += date.Year.ToString() + string.Format("{0:d2}", date.Month) + string.Format("{0:d2}", date.Minute) + rnd.Next(10, 99).ToString();
                strNewGuid = strNewGuid.Substring(2);

                return sPrefix + strNewGuid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string Get_New12ByteGuid()
        {
            return Get_New12ByteGuid("");
        }

        public static string Get_New12ByteGuid(string sPrefix)
        {
            try
            {
                var result = new StringBuilder();
                for (var i = 0; i < 12; i++)
                {
                    var r = new Random(Guid.NewGuid().GetHashCode());
                    result.Append(r.Next(0, 10));
                }

                return sPrefix + result.ToString().Replace('0','8');
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 0~9 A~Z字符串
        /// </summary>
        public static string RandomString_09AZ = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ";
        /// <summary>
        /// 依据指定字符串来生成随机字符串
        /// </summary>
        /// <param name="random">Random</param>
        /// <param name="randomString">指定字符串</param>
        /// <param name="size">字符串长度</param>
        /// <param name="lowerCase">字符串是小写</param>
        /// <returns>随机字符串</returns>
        public static string NextString(int size, bool lowerCase)
        {
            string _nextString = string.Empty;
            if (rnd != null && !string.IsNullOrEmpty(RandomString_09AZ))
            {
                StringBuilder _builder = new StringBuilder(size);
                int _maxCount = RandomString_09AZ.Length - 1;
                for (int i = 0; i < size; i++)
                {
                    int _number = rnd.Next(0, _maxCount);
                    _builder.Append(RandomString_09AZ[_number]);
                }
                _nextString = _builder.ToString();
            }
            return lowerCase ? _nextString.ToLower() : _nextString.ToUpper();
        }

        public static string Get_New20ByteGuid()
        {
            return Get_New20ByteGuid("");
        }

        public static string Get_New20ByteGuid(string sPrefix)
        {
            try
            {
                string strNewGuid = "";
                DateTime date = DateTime.Now;

                strNewGuid += date.Year.ToString() + string.Format("{0:d2}", date.Second) + string.Format("{0:d2}", date.Month) + string.Format("{0:d2}", date.Minute) + NextString(6, true) + rnd.Next(1000, 9999).ToString();

                return sPrefix + strNewGuid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string Get_New128ByteGuid()
        {
            try
            {
                string strNewGuid = Guid.NewGuid().ToString();
                return strNewGuid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string ToCustomDateString(string sDateString, string sDateFormat)
        {
            try
            {
                DateTime dDate = DateTime.Parse(sDateString);
                string sNewDate = "";

                switch (sDateFormat)
                {
                    case "YMD": sNewDate = dDate.Year.ToString() + "年" + dDate.Month.ToString() + "月" + dDate.Day.ToString() + "日"; break;
                    case "YM": sNewDate = dDate.Year.ToString() + "年" + dDate.Month.ToString() + "月"; break;
                    case "Y": sNewDate = dDate.Year.ToString() + "年"; break;
                    default: sNewDate = dDate.ToShortDateString(); break;
                }

                return sNewDate;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int GetRoundingDevision(int num1, int num2)
        {
            try
            {
                int temp = num1 % num2;
                if (temp > 0)
                {
                    temp = num1 / num2 + 1;
                }
                else
                {
                    temp = num1 / num2;
                }

                return temp;

            }
            catch
            {
                return 1;
            }
        }
    }
}
