using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.Framework.Utility
{
    public class CalendarHelper
    {
        /// <summary>
        /// 获取指定日期，在为一年中为第几周
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <reutrn>返回第几周</reutrn>
        public  static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        public static int GetDayOfWeek(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int dayOfWeek=1;
            DayOfWeek dw= gc.GetDayOfWeek(dt);
            switch(dw)
            {
                case DayOfWeek.Monday: dayOfWeek = 1; break;
                case DayOfWeek.Tuesday: dayOfWeek = 2; break;
                case DayOfWeek.Wednesday: dayOfWeek = 3; break;
                case DayOfWeek.Thursday: dayOfWeek = 4; break;
                case DayOfWeek.Friday: dayOfWeek = 5; break;
                case DayOfWeek.Saturday: dayOfWeek = 6; break;
                case DayOfWeek.Sunday: dayOfWeek = 0; break;
                default:dayOfWeek = 1; break;
            }
            return dayOfWeek;
        }

        public static string GetCnNameOfWeek(int weekday,bool isSimple=true)
        {
            if (isSimple)
            {
                string cnName = "周一";
                switch (weekday)
                {
                    case 0: cnName = "周日"; break;
                    case 1: cnName = "周一"; break;
                    case 2: cnName = "周二"; break;
                    case 3: cnName = "周三"; break;
                    case 4: cnName = "周四"; break;
                    case 5: cnName = "周五"; break;
                    case 6: cnName = "周六"; break;
                    default: break;
                }
                return cnName;
            }
            else
            {
                string cnName = "星期一";
                switch (weekday)
                {
                    case 0: cnName = "星期日"; break;
                    case 1: cnName = "星期一"; break;
                    case 2: cnName = "星期二"; break;
                    case 3: cnName = "星期三"; break;
                    case 4: cnName = "星期四"; break;
                    case 5: cnName = "星期五"; break;
                    case 6: cnName = "星期六"; break;
                    default: break;
                }
                return cnName;
            } 
        }

        public static string GetCNDayOfWeek(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            string dayOfWeek = "星期一";
            DayOfWeek dw = gc.GetDayOfWeek(dt);
            switch (dw)
            {
                case DayOfWeek.Monday: dayOfWeek = "星期一"; break;
                case DayOfWeek.Tuesday: dayOfWeek = "星期二"; break;
                case DayOfWeek.Wednesday: dayOfWeek = "星期三"; break;
                case DayOfWeek.Thursday: dayOfWeek = "星期四"; break;
                case DayOfWeek.Friday: dayOfWeek = "星期五"; break;
                case DayOfWeek.Saturday: dayOfWeek = "星期六"; break;
                case DayOfWeek.Sunday: dayOfWeek = "星期日"; break;
                default: dayOfWeek = "星期一"; break;
            }
            return dayOfWeek;
        }


        public static string GetCNMonthOfYear(int month)
        { 
            string monthOfYear = "一月份";
            switch (month)
            {
                case 0: monthOfYear = "一月份"; break;
                case 1: monthOfYear = "二月份"; break;
                case 2: monthOfYear = "三月份"; break;
                case 3: monthOfYear = "四月份"; break;
                case 4: monthOfYear = "五月份"; break;
                case 5: monthOfYear = "六月份"; break;
                case 6: monthOfYear = "七月份"; break;
                case 7: monthOfYear = "八月份"; break;
                case 8: monthOfYear = "九月份"; break;
                case 9: monthOfYear = "十月份"; break;
                case 10: monthOfYear = "十一月份"; break;
                case 11: monthOfYear = "十二月份"; break;
                default: monthOfYear = "一月份"; break;
            }
            return monthOfYear;
        } 
    }
}
