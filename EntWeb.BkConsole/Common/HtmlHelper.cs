using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntWeb.BkConsole
{
    /// <summary>
    /// HTML帮助类
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// 过滤字符，输出HTML格式数据
        /// </summary>
        public static MvcHtmlString RtfTextToHtml(string rtfText)
        {
            if (String.IsNullOrWhiteSpace(rtfText))
            {
                return new MvcHtmlString("");
            }
            else
            {
                var text = rtfText
                    .Replace(" ", "&nbsp;")
                    .Replace("\t", string.Format("{0}{0}{0}{0}", "&nbsp;"))
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\n", "<br/>");
                return new MvcHtmlString(text);
            }
        }

        /// <summary>
        /// 显示错层方法
        /// </summary>
        public static object GetDepartmentName(string name, decimal? level)
        {
            if (level > 1)
            {
                string nbsp = "&nbsp;&nbsp;";
                for (int i = 0; i < level; i++)
                {
                    nbsp += "&nbsp;&nbsp;";
                }
                name = nbsp + "|--" + name;
            }
            return name;
        }

        /// <summary>
        /// 普通列表分页结果
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">显示行数</param>
        /// <param name="count">总条数</param>
        /// <param name="url">跳转URL</param>
        /// <returns></returns>
        public static MvcHtmlString PaginationPager(int Page, int PageSize, int Count, int PageNum, string Url)
        {
            int index;
            int num = 0;
            //数据总条数小于等于显示行数
            if (Count <= PageSize)
            {
                return new MvcHtmlString("");
            }
            if (Count == 0)
            {
                return new MvcHtmlString("");
            }
            if ((Count % PageSize) == 0)
            {
                num = Count / PageSize;
            }
            else
            {
                num = (Count / PageSize) + 1;
            }
            if (string.IsNullOrEmpty(Url))
            {
                Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            }
            System.Text.StringBuilder str = new System.Text.StringBuilder(15000);
            //str.Append("<div class=\"black3\">");
            bool flag = false;
            if (Url.IndexOf("@p@") < 0)
            {
                index = Url.IndexOf("?");
                if ((index > 0) && (index < Url.Length))
                {
                    int length = Url.ToLower().IndexOf("page=", index);
                    if (length > 0)
                    {
                        int startIndex = Url.IndexOf("&", (int)(length + 1));
                        if (startIndex > 0)
                        {
                            Url = Url.Substring(0, length) + Url.Substring(startIndex);
                            Url = Url + "&";
                        }
                        else
                        {
                            Url = Url.Substring(0, length);
                        }
                    }
                    else
                    {
                        Url = Url + "&";
                    }
                }
                else
                {
                    Url = "?";
                }
            }
            str.Append("<div class=\"dataTables_paginate paging_full_numbers\" style=\"padding-top: 0px;\" id=\"example_paginate\">");
            str.Append("<ul class=\"pagination\">");
            str.Append("<div class=\"dataTables_paginate paging_bs_full\" id=\"datatable_paginate\">");
            str.Append("<ul class=\"pagination\">");
            //处理首页页码
            if (Page > 1)
            {
                //记录分页栏首个页码 index
                str.Append("<li class=\"paginate_button first\" id=\"example_first\">");
                str.Append("<a href=\""+ ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", "1") : (Url + "page=1")) +"\" aria-controls=\"example\" data-dt-idx=\"0\" tabindex=\"0\">首页</a></li>");
            }
            else
            {
                str.Append("<li class=\"paginate_button first disabled\" id=\"example_first\"><a href=\"\" aria-controls=\"example\" data-dt-idx=\"0\" tabindex=\"0\">首页</a></li>");//第一个分页栏中首页不可点击
            }
            //处理上一页页码
            if (Page > 1)
            {
                index = Page - 1;
                str.Append("<li class=\"paginate_button previous\" id=\"example_previous\">");
                str.Append("<a href=\""+ ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", index.ToString()) : (Url + "page=" + index.ToString())) +"\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">上一页</a></li>");
            }
            else
            {
                str.Append("<li class=\"paginate_button previous disabled\" id=\"example_previous\">");
                str.Append("<a href=\"\" aria-controls=\"example\" data-dt-idx=\"1\" tabindex=\"0\">上一页</a></li>");
            }
            str.Append("");
            //中间页码处理方式
            if ((Page + (PageNum / 2)) <= num)
            {
                index = (Page + (PageNum / 2)) - PageNum;
            }
            else
            {
                index = (num - PageNum) + 1;
            }
            if (index <= 0)
            {
                index = 1;
            }
            for (int i = 1; i <= PageNum; i++)
            {
                if (index > num)//非正常情况P传值
                {
                    break;
                }
                if (index != Page)
                {
                    str.Append("<li class=\"paginate_button \"><a href=\""+((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", index.ToString()) : (Url + "page=" + index.ToString())) +"\" aria-controls=\"example\" data-dt-idx=\"3\" tabindex=\"0\">" + index.ToString() + "</a></li>");
                    flag = true;//这里控制是不是只有一页，如果加了判断，只有一页数据，分页不显示
                }
                else
                {
                    str.Append("<li class=\"paginate_button active\"><a href=\"JavaScript:void(0)\" aria-controls=\"example\" data-dt-idx=\"2\" tabindex=\"0\">" + index.ToString() + "</a></li>");
                }
                index++;
            }
            //下一页
            if (Page < num)
            {
                index = Page + 1;
                str.Append("<li class=\"paginate_button next\" id=\"example_next\">");
                str.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", index.ToString()) : (Url + "page=" + index.ToString())) + "\"  aria-controls=\"example\" data-dt-idx=\"4\" tabindex=\"0\">下一页</a></li>");
            }
            else
            {
                str.Append("<li class=\"paginate_button next disabled\" id=\"example_next\"><a href=\"javascript:void(0)\" aria-controls=\"example\" data-dt-idx=\"4\" tabindex=\"0\">下一页</a></li>");
            }
            //末页
            if (Page < num)
            {
                str.Append("<li class=\"paginate_button last\" id=\"example_last\">");
                str.Append("<a href=\"" + ((Url.IndexOf("@p@") >= 0) ? Url.Replace("@p@", num.ToString()) : (Url + "page=" + num.ToString())) + "\"  aria-controls=\"example\" data-dt-idx=\"5\" tabindex=\"0\">末页</a></li>");
            }
            else
            {
                str.Append("<li class=\"paginate_button last disabled\"><a id=\"datatable_last\">末页</a></li>");
            }
            //尾注
            str.Append("</ul>");
            str.Append("</div>");
            //if (!flag)
            //{
            //    return "";
            //}
            return new MvcHtmlString(str.ToString());

        }
    }
}