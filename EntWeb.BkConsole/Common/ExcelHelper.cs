using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using NPOI.SS.Util;
using NPOI.HSSF.Util;

namespace EntWeb.BkConsole
{
    public class ExcelHelper
    {  //将Datatabel写入Excel流中
        public static Stream RenderDataTableToExcel(DataTable SourceTable, string sheetName, string tableTitle, string headerName, string footerName, string rfooterName, string lfooterName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);

            #region 设置页头页尾
            sheet.CreateRow(0).CreateCell(1).SetCellValue(123);
            sheet.Header.Center = headerName;
            sheet.Footer.Center = footerName;
            sheet.Footer.Right = rfooterName;
            sheet.Footer.Left = lfooterName;
            #endregion


            #region 合并单元格+设置样式
            int colNum = SourceTable.Columns.Count;
            //CellRangeAddress（）该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, colNum - 1));
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;
            ICell icell1top0 = row0.CreateCell(0);
            icell1top0.CellStyle = Getcellstyle(workbook, stylexls.header);
            icell1top0.SetCellValue(tableTitle);
            icell1top0.CellStyle.VerticalAlignment = VerticalAlignment.Center;
            icell1top0.CellStyle.Alignment = HorizontalAlignment.Center;
            #endregion

            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }

            // handling value. 
            int rowIndex = 2;
            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static Stream RenderDataTableToExcel(DataTable SourceTable, string sheetName, string tableTitle, string headerName, string footerName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);

            #region 设置页头页尾
            sheet.CreateRow(0).CreateCell(1).SetCellValue(123);
            sheet.Header.Center = headerName;
            sheet.Footer.Center = footerName;
            #endregion


            #region 合并单元格+设置样式
            int colNum = SourceTable.Columns.Count;
            //CellRangeAddress（）该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, colNum - 1));
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;
            ICell icell1top0 = row0.CreateCell(0);
            icell1top0.CellStyle = Getcellstyle(workbook, stylexls.header);
            icell1top0.SetCellValue(tableTitle);
            icell1top0.CellStyle.VerticalAlignment = VerticalAlignment.Center;
            icell1top0.CellStyle.Alignment = HorizontalAlignment.Center;
            #endregion

            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }

            // handling value. 
            int rowIndex = 2;
            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static Stream RenderDataTableToExcel(DataTable SourceTable, string sheetName, string tableTitle)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);

            #region 合并单元格,这是表名
            int colNum = SourceTable.Columns.Count;
            //CellRangeAddress（）该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, colNum - 1));
            IRow row0 = sheet.CreateRow(0);
            row0.Height = 20 * 20;
            ICell icell1top0 = row0.CreateCell(0);
            icell1top0.CellStyle = Getcellstyle(workbook, stylexls.header);
            icell1top0.SetCellValue(tableTitle);
            icell1top0.CellStyle.VerticalAlignment = VerticalAlignment.Center;
            icell1top0.CellStyle.Alignment = HorizontalAlignment.Center;
            #endregion

            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(1);
            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }

            // handling value. 
            int rowIndex = 2;
            foreach (DataRow row in SourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static DataTable ConvertCollToDataTable(object InfoColl, List<string> cellName)
        {
            return null;
        }
        #region 定义单元格常用到样式的枚举
        public enum stylexls
        {
            header,
            url,
            datetime,
            number,
            money,
            percent,
            uppercase,
            scnotation,
            mydefault
        }
        #endregion
        #region 定义单元格常用到样式
        public static ICellStyle Getcellstyle(IWorkbook wb, stylexls str)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();

            //定义几种字体  
            //也可以一种字体，写一些公共属性，然后在下面需要时加特殊的  
            IFont font12 = wb.CreateFont();
            font12.FontHeightInPoints = 10;
            font12.FontName = "微软雅黑";


            IFont font = wb.CreateFont();
            font.FontName = "微软雅黑";
            //font.Underline = 1;下划线  


            IFont fontcolorblue = wb.CreateFont();
            fontcolorblue.Color = HSSFColor.OliveGreen.Blue.Index;
            fontcolorblue.IsItalic = true;//下划线  
            fontcolorblue.FontName = "微软雅黑";


            //边框  
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Dotted;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Hair;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Hair;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Dotted;
            //边框颜色  
            cellStyle.BottomBorderColor = HSSFColor.OliveGreen.Blue.Index;
            cellStyle.TopBorderColor = HSSFColor.OliveGreen.Blue.Index;

            //背景图形，我没有用到过。感觉很丑  
            //cellStyle.FillBackgroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;  
            //cellStyle.FillForegroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;  
            cellStyle.FillForegroundColor = HSSFColor.White.Index;
            // cellStyle.FillPattern = FillPatternType.NO_FILL;  
            cellStyle.FillBackgroundColor = HSSFColor.Blue.Index;

            //水平对齐  
            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;

            //垂直对齐  
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            //自动换行  
            cellStyle.WrapText = true;

            //缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对  
            cellStyle.Indention = 0;

            //上面基本都是设共公的设置  
            //下面列出了常用的字段类型  
            switch (str)
            {
                case stylexls.header:
                    // cellStyle.FillPattern = FillPatternType.LEAST_DOTS;  
                    cellStyle.SetFont(font12);
                    break;
                case stylexls.datetime:
                    IDataFormat datastyle = wb.CreateDataFormat();

                    cellStyle.DataFormat = datastyle.GetFormat("yyyy/mm/dd");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.number:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.money:
                    IDataFormat format = wb.CreateDataFormat();
                    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.url:
                    fontcolorblue.Underline = FontUnderlineType.Single;
                    cellStyle.SetFont(fontcolorblue);
                    break;
                case stylexls.percent:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.uppercase:
                    IDataFormat format1 = wb.CreateDataFormat();
                    cellStyle.DataFormat = format1.GetFormat("[DbNum2][$-804]0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.scnotation:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.mydefault:
                    cellStyle.SetFont(font);
                    break;
            }
            return cellStyle;


        }
        #endregion
    }
}