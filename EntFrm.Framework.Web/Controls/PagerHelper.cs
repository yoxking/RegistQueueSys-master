/*
 ASP.NET MvcPager 分页组件
 Copyright:2009-2011 陕西省延安市吴起县 杨涛\Webdiyer (http://www.webdiyer.com)
 Source code released under Ms-PL license
 */

namespace EntFrm.Framework.Web.Controls
{
    public class PagerHelper
    {
        public int PageSize { get; set; }//每页显示记录数
        public int PageIndex { get; set; }// 当前页号
        public int PageCount { get; set; }// 总页数
        public int TotalCount { get; set; }// 记录总数
        public int[] pageNumbers { get; set; }

        public PagerHelper(int pageIndex, int pageSize, int totalCount)
        {
            this.TotalCount = totalCount;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;

            setTotalPageCountByRs();
        }

        /**
         * 设置显示页数集合
         * @param totalPageCount
         */
        public void setPageNumbers(int pageCount)
        {
            if (pageCount > 0)
            {
                int[] numbers = new int[pageCount > 10 ? 10 : pageCount];//页面要显示的页数集合
                int k = 0;
                for (int i = 0; i < pageCount; i++)
                {
                    //保证当前页为集合的中间
                    if ((i >= PageIndex - (numbers.Length / 2 + 1) || i >= pageCount - numbers.Length) && k < numbers.Length)
                    {
                        numbers[k] = i + 1;
                        k++;
                    }
                    else if (k >= numbers.Length)
                    {
                        break;
                    }
                }
                this.pageNumbers = numbers;
            }
        }

        /** 
         * 得到开始记录数
         * @return
         */
        public int getStartRow()
        {
            return (PageIndex - 1) * PageSize;
        }

        /**
         * 得到结束记录数
         * @return
         */
        public int getEndRow()
        {
            return PageIndex * PageSize;
        }

        /**
         * @param totalCount
         *  The totalCount to set.
         */
        public void setTotalCount(int totalCount)
        {
            if (totalCount >= 0)
            {
                this.TotalCount = totalCount;
                setTotalPageCountByRs();//根据总记录数计算总页数

                /*if (this.index > this.totalPageCount) {
                    this.index = this.totalPageCount;
                }*/
            }
        }


        private void setTotalPageCountByRs()
        {
            if (this.PageSize > 0 && this.TotalCount > 0 && this.TotalCount % this.PageSize == 0)
            {
                this.PageCount = this.TotalCount / this.PageSize;
            }
            else if (this.PageSize > 0 && this.TotalCount > 0 && this.TotalCount % this.PageSize > 0)
            {
                this.PageCount = (this.TotalCount / this.PageSize) + 1;
            }
            else
            {
                // this.totalPageCount = 1;
                this.PageCount = 0;
            }
            setPageNumbers(PageCount);//获取展示页数集合
        }


        /*
            public static int getTotalPageCount(int iTotalRecordCount, int iPageSize) {
                if (iPageSize == 0) {
                    return 0;
                } else {
                    return (iTotalRecordCount % iPageSize) == 0 ? (iTotalRecordCount / iPageSize) : (iTotalRecordCount / iPageSize) + 1;
                }
            }*/
    }

    //#region Html Pager
    //public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName)
    //{
    //    //var totalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
    //    //var builder = new PagerBuilder
    //    //    (
    //    //        helper,
    //    //        actionName,
    //    //        controllerName,
    //    //        totalPageCount,
    //    //        pageIndex,
    //    //        totalItemCount,
    //    //        pagerOptions,
    //    //        routeName,
    //    //        new RouteValueDictionary(routeValues),
    //    //        new RouteValueDictionary(htmlAttributes)
    //    //    );
    //    //return builder.RenderPager();
    //    return null;
    //}
}