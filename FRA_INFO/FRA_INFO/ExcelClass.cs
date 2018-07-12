using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

namespace FRA_INFO
{
    public class ExcelClass
    {


        static String filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "7373.xlsx";//@"D:\Data2.xlsx"

        /// <summary>
        /// 从textbox保存数据到数组，再讲数组保存到excel
        /// </summary>
        /// <param name="lines">数组</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="col">列号</param>
        public static void SaveXls(string filePath, string[] lines, int rowIndex, int col)
        {
            Debug.WriteLine("========SaveXLS======");
            //启动Excel应用程序  
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            //新建工作簿           
            Workbook xBook = xls.Workbooks.Add(Missing.Value);
            //新建工作表  
            Worksheet xSheet = (Worksheet)xBook.Sheets[1];
            //判断exce是否存在，若存在则删除  
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    //MessageBox.Show("文件存在", "SaveXLS");
                }
                else
                {
                    //MessageBox.Show("文件不存在！！！", "SaveXLS");
                    File.Create(filePath);
                    Debug.WriteLine("=======filePath===========" + filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SaveXLS");
                return;
            }

            int rowNum = lines.Length; //读取数组的长度
            Debug.WriteLine("========rowNum======" + rowNum);

            for (int i = 0; i < rowNum; i++)//将数据导入Excel      
            {
                rowIndex++;
                xls.Cells[rowIndex, col] = Convert.ToString(lines[i].ToString()) + "\t";//读取到的Textbox控件里的数据导入到Excel                
            }
            //保存excel
            xSheet.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            xSheet = null;
            xBook = null;
            xls.Quit(); //这一句是非常重要的，否则Excel对象不能从内存中退出 
            xls = null;
            //xBook.Close();
            MessageBox.Show("数据导入成功！！");
        }

        /// <summary>
        /// 从Excel读取数据
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="index">读取第index个sheet的数据 </param>
        /// <returns></returns>
        public static Array ReadXls(string filename, int index)//读取第index个sheet的数据  
        {
            Debug.WriteLine("=======ReadXls===========");
            //启动Excel应用程序  
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            //打开filename表  
            _Workbook book;
            try
            {
                book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ReadXls");
                return null;
            }
            _Worksheet sheet;//定义sheet变量  
            xls.Visible = false;//设置Excel后台运行  
            xls.DisplayAlerts = false;//设置不显示确认修改提示  

            //if (!(File.Exists(filename)))
            //{
            //    MessageBox.Show("目标文件不存在！！");
            //}

            try
            {
                sheet = (_Worksheet)book.Worksheets.get_Item(index);//获得第index个sheet，准备读取  
            }
            catch (Exception ex)//不存在就退出  
            {
                MessageBox.Show(ex.ToString(), "ReadXls");
                return null;
            }

            // Console.WriteLine(sheet.Name);
            int row = sheet.UsedRange.Rows.Count;//获取不为空的行数  
            int col = sheet.UsedRange.Columns.Count;//获取不为空的列数 
            // Array value = (Array)sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[row, col]).Cells.Value2;//获得区域数据赋值给Array数组，方便读取

            Range range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[row, col]];
            Array value = (Array)range.Value2;
            book.Save();//保存  
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表  
            xls.Quit();//Excel程序退出  
                       //sheet,book,xls设置为null，防止内存泄露  
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源  
            return value;
        }

        /// <summary>
        /// 数组保存到excel
        /// </summary>
        /// <param name="filePath">excel路径</param>
        /// <param name="arrayOne">数组1</param>
        /// <param name="arrayTwo">数组2</param>
        /// <param name="arrayThree">数组3</param>
        public static void ExportTasks(string filePath, string[] arrayOne, string[] arrayTwo, string[] arrayThree)
        {
            Debug.WriteLine("=======ExportTasks===========");
            // 定义Application 对象,此对象表示整个Excel 程序
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            // 定义Workbook对象,此对象代表工作薄
            Microsoft.Office.Interop.Excel.Workbook workBook;
            // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
            Microsoft.Office.Interop.Excel.Worksheet ws = null;

            //定义Range对象,此对象代表单元格区域
            try
            {
                //初始化 Application 对象 excelApp
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                //在工作薄的第一个工作表上创建任务列表
                workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = (Worksheet)workBook.Worksheets[1];
                // 命名工作表的名称为 "Task Management"
                ws.Name = "文件名称";
                // 居中 
                ws.Rows.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // 单元格的宽度
                ((Microsoft.Office.Interop.Excel.Range)ws.Columns["A", Type.Missing]).ColumnWidth = 15;
                ((Microsoft.Office.Interop.Excel.Range)ws.Columns["B", Type.Missing]).ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Range)ws.Columns["C", Type.Missing]).ColumnWidth = 20;
                //第一行
                // ws.Cells[1, 1] = "编号";
                // ws.Cells[1, 2] = "one";
                // ws.Cells[1, 3] = "tpw";
                // ws.Cells[1, 4] = "three";
                /// 填充单元格
                for (int i = 0; i < arrayOne.Length; i++)
                {
                    // ws.Cells[1 + i, 1] = i + 1;
                    ws.Cells[1 + i, 1] = arrayOne[i].ToString();
                    ws.Cells[1 + i, 2] = arrayTwo[i].ToString();
                    ws.Cells[1 + i, 3] = arrayThree[i].ToString();

                    //if ((arrayOne.Count & 7) == 0)   //如果arrayone 被 8 除 空一行 
                    //{
                    //    ws.Cells[3 + i, 1] = "";
                    //    ws.Cells[3 + i, 2] = "";
                    //    ws.Cells[3 + i, 3] = "";
                    //    ws.Cells[3 + i, 4] = "";
                    //}
                }
                excelApp.DisplayAlerts = false;//设置不显示确认修改提示  
                //string path = "";
                // String time = DateTime.Now.ToString();
                // time = time.Replace(":", "-");
                // path = fileNam3 + time;
                ws.SaveAs(filePath,
                            Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                             Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //保存后是否打开 Excel
                excelApp.Visible = false;//设置Excel后台运行                      
                excelApp.Quit();// 去掉  任务管理器的进程    
                workBook = null;
                //Form1.KillExecl("excelApp");
                MessageBox.Show("保存成功", "ExportTasks");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ExportTasks");
            }
        }

        /// <summary>
        /// 保存datatable数据到excel
        /// </summary>
        /// <param name="tmpDataTable">datatable</param>
        /// <param name="strFileName">excel路径</param>
        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 0;
            int columnIndex = 0;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
          //  foreach (DataColumn dc in tmpDataTable.Columns)
           // {
           //     columnIndex++;
           //     xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
          //  }

            //将DataTable中的数据导入Excel中
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                   
                }
            }
            //xlBook.SaveCopyAs(HttpUtility.UrlDecode(strFileName, System.Text.Encoding.UTF8));
            xlBook.SaveCopyAs(strFileName);
        }
    }
}




